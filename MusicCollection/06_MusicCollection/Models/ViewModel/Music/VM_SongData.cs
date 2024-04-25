namespace _06_MusicCollection.Models.ViewModel.Music
{
    public class VM_SongData
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Poster { get; set; }
        public string FilePath { get; set; }
        public double Duration { get; set; }
        public ICollection<int> ArtistIds { get; set; }
        public ICollection<int> AlbumIds { get; set; }
    }
}
