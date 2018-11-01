using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogSabrijaGolic.Models
{
    public class BlogPost
    {
        public int ID { get; set; }
        public string Slug { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Body { get; set; }
        public List<BlogPostTag> tagList { get; set; }
        public DateTime CratedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

    }
}
