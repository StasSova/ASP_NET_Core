using _06_MusicCollection.Models.ViewModel.Profile;

namespace _06_MusicCollection.Services.Language
{
    public interface ILangRead
    {
        List<VM_Language> languageList();
    }
    public class ReadLangServices : ILangRead
    {
        IConfiguration _con;
        List<VM_Language> languageLists;
        public ReadLangServices(IConfiguration con)
        {
            string section = "Lang";
            _con = con;
            IConfigurationSection pointSection = _con.GetSection(section);
            List<VM_Language> lists = new List<VM_Language>();
            foreach (var language in pointSection.AsEnumerable())
            {
                if (language.Value != null)
                    lists.Add(new VM_Language
                    {
                        ShortName = language.Key.Replace(section + ":", ""),
                        Name = language.Value
                    });
            }

            languageLists = lists;
        }

        public List<VM_Language> languageList() => languageLists;
    }
}
