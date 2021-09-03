using GrpcService.Repositories.EF.Models;
using Microsoft.EntityFrameworkCore;

namespace GrpcService.Repositories.EF
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public AppDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}