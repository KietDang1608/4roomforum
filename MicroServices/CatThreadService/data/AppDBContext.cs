using Microsoft.EntityFrameworkCore;
using MicroServices.CatThreadService.model;

namespace MicroServices.CatThreadService.data
{
    public class AppDBContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            const string connectionString = "server=localhost;database=forum_cat_thread;user=root;password=;";
            optionsBuilder.UseMySQL(connectionString, ServerVersion.AutoDetect(connectionString));
        }
    }
}
