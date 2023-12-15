namespace FinalProject1.Models
{
    public class User: BaseEntity
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string ProfilePicture { get; set; }
        public string Bio { get; set; }
    }
}
