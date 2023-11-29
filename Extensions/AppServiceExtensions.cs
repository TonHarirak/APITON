using System.Text;
using APITON.Data;
using APITON.Interface;
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

        return services;
    }
}