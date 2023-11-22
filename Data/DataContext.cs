using APITON.Entities;
using Microsoft.EntityFrameworkCore;

namespace APITON.Data;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions options) : base(options)
    {
    }
    public DbSet<AppUser> Users { get; set; }
}