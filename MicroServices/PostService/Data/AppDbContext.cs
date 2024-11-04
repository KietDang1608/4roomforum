using Microsoft.EntityFrameworkCore;
using PostService.Models;
using System.Collections.Generic;

namespace PostService.Data
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
        {
        }

        public DbSet<Post> Posts{ get; set; }
    }
}
