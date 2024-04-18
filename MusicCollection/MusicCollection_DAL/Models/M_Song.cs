namespace MusicCollection_DAL.Models
{
    public class M_Song : M_DbEntity
    {
        public required string Title { get; set; }
        public required string Poster { get; set; } = "https://cdn.pixabay.com/photo/2014/04/02/14/04/vinyl-306070_1280.png";
        public required string FilePath { get; set; }
        public TimeSpan Duration { get; set; }
        public int Likes { get; set; } = 0;
        public int Plays { get; set; } = 0;
        public int Downloads { get; set; } = 0;
        public virtual ICollection<M_Genre>? Genres { get; set; }
        public virtual ICollection<M_Artist>? Artists { get; set; }
        public int AlbumId { get; set; }
        public virtual M_Album? Album { get; set; }
    }
}
