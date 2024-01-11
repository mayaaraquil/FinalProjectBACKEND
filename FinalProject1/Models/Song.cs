namespace FinalProject1.Models
{
    public class Song: BaseEntity
    {
        public int SongId { get; set; }
        public int UserId {  get; set; }
        public string SpotifySongId { get; set; }
        //enum 3
    }
}
