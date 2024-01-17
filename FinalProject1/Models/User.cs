using System.ComponentModel.DataAnnotations;

namespace FinalProject1.Models
{
    public class EndUser: BaseEntity
    {
        [Key]
        public int userId { get; set; }
        public string authName { get; set; }
        public string username { get; set; }
        public string profilePicture { get; set; }
        public string bio { get; set; }
    }
}
