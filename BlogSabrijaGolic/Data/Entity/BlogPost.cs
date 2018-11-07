using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BlogSabrijaGolic.Models
{
    public class BlogPost

    {
        public int ID { get; set; }
        public string Slug { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Body { get; set; }
        public List<BlogPostTag> TagList { get; set; }
        public DateTime CratedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

    }
}
