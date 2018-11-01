using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogSabrijaGolic.Models
{
    public class BlogPostTag
    {
        public int ID { get; set; }
        public Tag Tag { get; set; }
        public int TagID { get; set; }
        public BlogPost BlogPost { get; set; }
        public int BlogPostID { get; set; }
    }
}
