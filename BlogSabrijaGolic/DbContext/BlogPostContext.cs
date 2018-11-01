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
    }
}
