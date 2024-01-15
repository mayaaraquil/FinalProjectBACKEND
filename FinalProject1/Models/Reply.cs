using System.ComponentModel.DataAnnotations.Schema;

namespace FinalProject1.Models
{
    public class Reply
    {

        public int ReplyId { get; set; }

        [ForeignKey("ParentCommentId")]
        public int ParentCommentId { get; set; }
        public string ReplyText { get; set; }
        public string AuthorId { get; set; }
    }
}
