using System.ComponentModel.DataAnnotations;

namespace FinalProject1.Models
{
    public class BlogPost: BaseEntity
    { 
        [Key]
        public int BlogId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public int UserId { get; set; }
        //enum 1
    }
}
