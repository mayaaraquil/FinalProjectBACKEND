using System.ComponentModel.DataAnnotations;

namespace FinalProject1.Models
{
    public class Video : BaseEntity
    {
        [Key]
        public int VideoId { get; set; }
        public int UserId {  get; set; }
        public string VideosUrl { get; set; }
        //enum 4
    }
}
