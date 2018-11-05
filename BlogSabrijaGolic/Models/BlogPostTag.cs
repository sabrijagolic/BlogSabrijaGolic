namespace BlogSabrijaGolic.Models
{
    public class BlogPostTag
    {
        public int ID { get; set; }
        public Tag Tag { get; set; }
        public int TagId { get; set; }
        public BlogPost BlogPost { get; set; }
        public int BlogPostId { get; set; }
    }
}