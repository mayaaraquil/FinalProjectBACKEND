

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinalProject1.Models
{
    public class Comments: BaseEntity
    {
        //main table comment to posts
        [Key]
        public int CommentId { get; set; } //creates a comment id that is likely unique
        public string CommentText { get; set; }
        public int UserId { get; set; } //refernces user table
        public PostTypes PostType { get; set; } //tells what post type we are working with, song, video, etc

        //foreign keys to the PostTypes databases
        [ForeignKey("BlogId")]
        public int? BlogId { get; set; }
        


        [ForeignKey("PlaylistId")]
        public int? PlaylistId { get; set; }



        [ForeignKey("SongId")]
        public int? SongId { get; set; }


        [ForeignKey("VideoId")]       
        public int? VideoId { get; set; }
      
 
    }
}
