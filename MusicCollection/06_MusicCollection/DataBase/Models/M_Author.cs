namespace _06_MusicCollection.DataBase.Models
{
    public class M_Author : M_DbEntity
    {
        public string Name { get; set; }
        public ICollection<M_Song> Songs { get; set; }
    }
}
