namespace MusicCollection_DAL.Models
{
    public class M_Genre : M_DbEntity
    {
        public required string Name { get; set; }
        public string Poster { get; set; } = "https://cdn.venngage.com/template/thumbnail/small/bf008bfe-9bf6-4511-b795-e86f070bfff5.webp";
        public string BackgroundColor { get; set; } = "#fff";
        public virtual ICollection<M_Song>? Songs { get; set; }
    }
}
