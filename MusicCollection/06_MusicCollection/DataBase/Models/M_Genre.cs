namespace _06_MusicCollection.DataBase.Models
{
    public class M_Genre : M_DbEntity
    {
        public string Name { get; set; }
        public string Poster { get; set; }
        public string BackgroundColor { get; set; }
        public ICollection<M_Song> Songs { get; set; }
    }
}
