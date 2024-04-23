using _06_MusicCollection.Models.ViewModel.Tags;
using MusicCollection_BLL.DTO;

namespace _06_MusicCollection.Models.ViewModel.Home
{
    public class VM_Musics
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Poster { get; set; }
        public string? Description { get; set; }
        public int TotalSongs { get; set; } = 0;
        public int TotalLikes { get; set; } = 0;
        public TimeSpan TotalDuration { get; set; } = TimeSpan.Zero;
        public ICollection<T_Song>? Songs { get; set; }


        public VM_Sort Sort { get; set; }
        public VM_Pagination Pagination { get; set; }
        public VM_Musics()
        {
        }
    }
}
