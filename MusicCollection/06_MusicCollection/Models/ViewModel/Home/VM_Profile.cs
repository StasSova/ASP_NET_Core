using _06_MusicCollection.Models.ViewModel.Profile;
using _06_MusicCollection.Services.Language;

namespace _06_MusicCollection.Models.ViewModel.Home
{
    public class VM_Profile
    {
        public ILangRead LangService;
        public VM_Profile(ILangRead serv)
        {
            LangService = serv;

            Languages = LangService.languageList();
        }
        public string Title { get; set; }
        public ICollection<VM_Language> Languages { get; set; }

    }
}
