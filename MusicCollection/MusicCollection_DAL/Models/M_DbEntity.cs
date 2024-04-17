namespace MusicCollection_DAL.Models
{
    public abstract class M_DbEntity
    {
        public int Id { get; set; }
        public DateOnly UploadDate { get; set; } = new DateOnly();
        public DateOnly DataUpdate { get; set; } = new DateOnly();
        public int? UploaderId { get; set; }
        public M_User? Uploader { get; set; }
    }
}
