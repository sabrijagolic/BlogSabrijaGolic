namespace BlogSabrijaGolic.Models
{
    public class BlogPostTag
    {
        
        public Tag Tag { get; set; }
        public int TagId { get; set; }
        public BlogPost BlogPost { get; set; }
        public int BlogPostId { get; set; }
    }
}