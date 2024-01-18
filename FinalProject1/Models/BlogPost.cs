using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinalProject1.Models
{
    public class BlogPost: BaseEntity
    { 
        [Key]
        public int BlogId { get; set; }
        [JsonProperty("blogTitle")]
        public string BlogTitle { get; set; }
        [JsonProperty("blogContent")]
        public string BlogContent { get; set; }
        public string UserId { get; set; }
        //enum 1
    }
}
