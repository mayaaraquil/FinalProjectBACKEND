using System.ComponentModel.DataAnnotations;

namespace FinalProject1.Models
{
    public class Likes: BaseEntity
    {
        [Key]
        public int LikeId { get; set; }
        public int UserId { get; set; }
        public int LikedItemId { get; set; }
        public PostTypes Posts { get; set; }

    }
}
