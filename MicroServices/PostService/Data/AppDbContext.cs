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
        public DbSet<Reply> Replies { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Reply>()
                .HasOne(r =>  r.Post)
                .WithMany(p => p.Replies)
                .HasForeignKey(r => r.PostId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Reply>()
                .HasOne(r => r.ReplyToReply2)
                .WithMany(r2 => r2.ReplyToReplies)
                .HasForeignKey(r => r.ReplyToReply)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
