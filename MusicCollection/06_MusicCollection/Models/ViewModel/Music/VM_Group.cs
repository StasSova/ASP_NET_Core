using MusicCollection_BLL.DTO;

namespace _06_MusicCollection.Models.ViewModel.Music
{
    public class VM_Group
    {
        public string Name { get; set; }
        public ICollection<T_Album> Albums { get; set; }
    }
}
