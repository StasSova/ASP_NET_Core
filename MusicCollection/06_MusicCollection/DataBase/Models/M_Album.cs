namespace _06_MusicCollection.DataBase.Models
{
    public class M_Album : M_DbEntity
    {
        public string Title { get; set; }
        public M_User User { get; set; }
        public string Poster { get; set; }
        public ICollection<M_Song> Songs { get; set; }
    }
}
