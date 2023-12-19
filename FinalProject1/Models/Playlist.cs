namespace FinalProject1.Models
{
    public class Playlist: BaseEntity
    {
        public int PlaylistId { get; set; }
        public string SpotifyPlaylistId { get; set; }

        public string PlaylistName { get; set;}
        //enum 2
    }
}
