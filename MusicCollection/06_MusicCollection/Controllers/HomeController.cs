using _06_MusicCollection.Models;
using _06_MusicCollection.Models.ViewModel.Home;
using _06_MusicCollection.Models.ViewModel.Music;
using Microsoft.AspNetCore.Mvc;
using MusicCollection_BLL.DTO;
using MusicCollection_BLL.Interfaces.Music;
using System.Diagnostics;

namespace _06_MusicCollection.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        IMusicService _musicService;
        public HomeController(ILogger<HomeController> logger, IMusicService musicService)
        {
            _logger = logger;
            _musicService = musicService;
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

            return View(viewData);
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public async Task<IActionResult> MusicsByAlbum(int id)
        {
            try
            {
                // ERROR при первом рпоходе все хорошо, почему-то автоматически вызвается ещё раз метод и в id идет 0
                if (id == 0) return Error();


                VM_Musics viewData = new VM_Musics();
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


                return View("Musics");
            }
            catch
            {
                return Error();
            }
        }
    }
}
