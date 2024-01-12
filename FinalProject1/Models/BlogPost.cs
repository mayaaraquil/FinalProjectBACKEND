using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinalProject1.Models
{
    public class BlogPost: BaseEntity
    { 
        [Key]
        public int BlogId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string AuthZeroUserId { get; set; }
        //enum 1
    }
}
