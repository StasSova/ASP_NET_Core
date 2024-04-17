namespace MusicCollection_DAL.Models
{
    public class M_Artist : M_DbEntity
    {
        public required string Name { get; set; }
        public ICollection<M_Song>? Songs { get; set; }
    }
}
