using Microsoft.EntityFrameworkCore;
using MicroServices.CatThreadService;
using Pomelo.EntityFrameworkCore;
namespace MicroServices.CatThreadService.Data
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }
    }
}
