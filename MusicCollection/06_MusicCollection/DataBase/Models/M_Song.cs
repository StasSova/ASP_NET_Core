namespace _06_MusicCollection.DataBase.Models
{
    public class M_Song : M_DbEntity
    {
        public string Title { get; set; }
        public string Poster { get; set; }
        public string FilePath { get; set; }
        public TimeSpan Duration { get; set; }
        public DateTime UploadDate { get; set; }
        public int Likes { get; set; }
        public int Plays { get; set; }
        public int Downloads { get; set; }
        public ICollection<M_Genre> Genres { get; set; }
        public ICollection<M_Author> Authors { get; set; }
        public M_User Uploader { get; set; }
    }
}
