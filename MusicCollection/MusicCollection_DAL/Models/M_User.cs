namespace MusicCollection_DAL.Models
{
    public class M_User : M_DbEntity
    {
        public M_User() { }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
        public int StatusId { get; set; }
        public M_UserStatus? Status { get; set; }
        public ICollection<M_Song>? UploadedSongs { get; set; }
        public ICollection<M_Album>? UploadedAlbums { get; set; }
        public ICollection<M_Genre>? UploadedGenres { get; set; }
        public ICollection<M_Artist>? UploadedArtists { get; set; }
        public ICollection<M_UserStatus> UploadedStatuses { get; set; }
    }
}
