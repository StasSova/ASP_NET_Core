using _06_MusicCollection.Attributes;
using _06_MusicCollection.Models;
using _06_MusicCollection.Models.TagsHelper;
using _06_MusicCollection.Models.ViewModel.Home;
using _06_MusicCollection.Models.ViewModel.Music;
using _06_MusicCollection.Models.ViewModel.Tags;
using _06_MusicCollection.Services.Language;
using Microsoft.AspNetCore.Mvc;
using MusicCollection_BLL.DTO;
using MusicCollection_BLL.Interfaces.Music;
using System.Diagnostics;

namespace _06_MusicCollection.Controllers
{
    [Culture]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        IMusicService _musicService;
        ILangRead _langService;
        public HomeController(ILogger<HomeController> logger, IMusicService musicService, ILangRead lang)
        {
            _logger = logger;
            _musicService = musicService;
            _langService = lang;
        }
        public async Task<IActionResult> Index()
        {
            VM_Index viewData = new VM_Index();
            viewData.Groups = new List<VM_Group>();

            var albums = await _musicService.GetPopularAlbums();
            var group = new VM_Group()
            {
                Name = "Popular albums",
                Albums = albums
            };
            viewData.Groups.Add(group);


            HttpContext.Session.SetString("path", Request.Path);
            return View(viewData);
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public async Task<IActionResult> MusicsByAlbum(int id, int page = 1, SortState sortOrder = SortState.TitleAsc)
        {
            try
            {
                // ERROR при первом рпоходе все хорошо, почему-то автоматически вызвается ещё раз метод и в id идет 0
                if (id == 0) return Error();

                int pageSize = 5;
                VM_Musics viewData = new VM_Musics();
                viewData.Id = id;


                T_Album alb = await _musicService.GetAlbumById(id);
                viewData.Title = alb.Title;
                viewData.Poster = alb.Poster;
                viewData.Description = $"Songs of {viewData.Title}";

                ICollection<T_Song> songs = await _musicService.GetSongsByAlbumId(id);
                ICollection<T_Song> temm = new List<T_Song>();

                if (songs != null)
                {
                    try
                    {
                        for (int i = 0; i < 10; i++)
                        {
                            foreach (T_Song song in songs)
                            {
                                viewData.TotalDuration += song.Duration;
                                viewData.TotalLikes += song.Likes;
                                viewData.TotalSongs++;
                                temm.Add(song);
                            }
                        }
                    }
                    catch { }
                }

                //viewData.Songs = songs;
                viewData.Songs = temm;

                // сортировка
                viewData.Songs = sortOrder switch
                {
                    SortState.TitleDesc => viewData.Songs.OrderByDescending(s => s.Title).ToList(),
                    SortState.TitleAsc => viewData.Songs.OrderBy(s => s.Title).ToList(),
                    SortState.AlbumDesc => viewData.Songs.OrderByDescending(s => s.Album).ToList(),
                    SortState.AlbumAsc => viewData.Songs.OrderBy(s => s.Album).ToList(),
                    SortState.DurationDesc => viewData.Songs.OrderByDescending(s => s.Duration).ToList(),
                    SortState.DurationAsc => viewData.Songs.OrderBy(s => s.Duration).ToList(),
                    _ => viewData.Songs.OrderBy(x => x.Title).ToList()
                };

                // пагинация 
                var count = viewData.Songs.Count;
                viewData.Songs = viewData.Songs.Skip((page - 1) * pageSize).Take(pageSize).ToList();


                viewData.Sort = new VM_Sort(sortOrder);
                viewData.Pagination = new VM_Pagination(count, page, pageSize);

                HttpContext.Session.SetString("path", Request.Path);
                return View("Musics", viewData);
            }
            catch
            {
                return Error();
            }
        }
        public async Task<IActionResult> MusicsByArtist(int id)
        {
            try
            {

                HttpContext.Session.SetString("path", Request.Path);
                return View("Musics");
            }
            catch
            {
                return Error();
            }
        }

        public async Task<IActionResult> Profile()
        {
            try
            {
                VM_Profile viewData = new VM_Profile(_langService);



                HttpContext.Session.SetString("path", Request.Path);
                return View(viewData);
            }
            catch
            {
                return Error();
            }
        }

        public ActionResult ChangeCulture(string lang)
        {
            string? returnUrl = HttpContext.Session.GetString("path") ?? "/Home/Index";

            // Список культур
            List<string> cultures = _langService.languageList().Select(t => t.ShortName).ToList()!;
            if (!cultures.Contains(lang))
            {
                lang = "ru";
            }

            CookieOptions option = new CookieOptions();
            option.Expires = DateTime.Now.AddDays(10); // срок хранения куки - 10 дней
            Response.Cookies.Append("lang", lang, option); // создание куки
            return Redirect(returnUrl);
        }

    }
}
