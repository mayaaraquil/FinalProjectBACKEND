namespace FinalProject1.Models
{
    public class Posts : BaseEntity
    {
        public int PostId { get; set; }
        public PostTypes PostType { get; set; }
    }
}
