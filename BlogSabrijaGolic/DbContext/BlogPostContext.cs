using BlogSabrijaGolic.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogSabrijaGolic.Models
{
    public class BlogPostContext : DbContext
    {
        public BlogPostContext(DbContextOptions<BlogPostContext> options): base(options)
        {
        }

        public DbSet<BlogPost> BlogPost { get; set; }
        public DbSet<Tag> Tag { get; set; }
        public DbSet<BlogPostTag> BlogPostTags { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Filename=Blog.db");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BlogPostTag>().HasKey(bc => new { bc.BlogPostId, bc.TagId });
            modelBuilder.Entity<BlogPostTag>().HasOne(t => t.BlogPost).WithMany(x => x.TagList).HasForeignKey(k => k.BlogPostId);
            modelBuilder.Entity<BlogPostTag>().HasOne(t => t.Tag).WithMany(x => x.BlogPostTag).HasForeignKey(k => k.TagId);            
            base.OnModelCreating(modelBuilder);
        }
    }
}
