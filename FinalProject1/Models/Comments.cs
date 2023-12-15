namespace FinalProject1.Models
{
    public class Comments: BaseEntity
    {
        public int CommentId { get; set; }
        public string CommentText { get; set; }
        public int UserId { get; set; }
        public Posts PostType { get; set; }
        public int PostId { get; set; }
    }
}
