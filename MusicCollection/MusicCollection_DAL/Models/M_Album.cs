namespace MusicCollection_DAL.Models
{
    public class M_Album : M_DbEntity
    {
        public required string Title { get; set; }
        public string Poster { get; set; } = "https://img.freepik.com/free-vector/realistic-music-record-label-disk-mockup_1017-33906.jpg?size=626&ext=jpg&ga=GA1.1.1908636980.1711929600&semt=ais";
        public virtual ICollection<M_Artist>? Artists { get; set; }
        public virtual ICollection<M_Song>? Songs { get; set; }
    }
}
