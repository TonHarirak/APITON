using System.Text;
using APITON.ClassLibrary1;
using APITON.Data;
using APITON.Helpers;
using APITON.Interface;
using APITON.Interfaces;
using APITON.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace APITON.Extensions;

public static class AppServiceExtensions
{
    public static IServiceCollection AddAppServices(this IServiceCollection services, IConfiguration conf)
    {
        services.AddDbContext<DataContext>(opt =>
        {
            opt.UseSqlite(conf.GetConnectionString("DefaultConnection"));
        });
        services.AddCors();
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<LogUserActivity>();
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        services.Configure<CloudinarySettings>(conf.GetSection("CloudinarySettings"));
        services.AddScoped<IImageService, ImageService>();
        services.AddScoped<IlikesRepository, LikesRepository>();
        services.AddScoped<IMessageRepository, MessageRepository>();


        return services;
    }
}