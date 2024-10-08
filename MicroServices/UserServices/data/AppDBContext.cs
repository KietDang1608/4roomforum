using Microsoft.EntityFrameworkCore;
using MicroServices.UserServices;
using Pomelo.EntityFrameworkCore;
namespace MicroServices.UserServices.Data
// namespace UserServices.data
{
    public class AppDBContext : DbContext
    {
         public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
    }
}