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
            modelBuilder.Entity<BlogPost>().HasMany(t => t.TagList).WithOne(x => x.BlogPost).HasForeignKey(k => k.BlogPostId);
            modelBuilder.Entity<Tag>().HasMany(t => t.BlogPostTag).WithOne(x => x.Tag).HasForeignKey(k => k.TagId);
            base.OnModelCreating(modelBuilder);
        }
    }
}
