using _06_MusicCollection.Models.ViewModel.Home;
using _06_MusicCollection.Models.ViewModel.Managment;
using _06_MusicCollection.Models.ViewModel.Music;
using _06_MusicCollection.Services.Language;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MusicCollection_BLL.DTO;
using MusicCollection_BLL.Interfaces.Music;
using Newtonsoft.Json;

namespace _06_MusicCollection.Controllers
{
    public class ManagementController : Controller
    {

        IMusicService _musicService;
        ILangRead _langService;
        IWebHostEnvironment _webHostEnvironment;
        public ManagementController(IMusicService musicService, ILangRead lang, IWebHostEnvironment webHostEnvironment)
        {
            _musicService = musicService;
            _langService = lang;
            _webHostEnvironment = webHostEnvironment;
        }
        // GET: Management
        public ActionResult Index()
        {
            return View("Musics");
        }

        public async Task<IActionResult> Musics()
        {
            return View();
        }
        public async Task<IActionResult> GetSongs()
        {
            ICollection<T_Song> song = await _musicService.GetSongs();
            if (song == null)
            {
                return NotFound();
            }
            string response = JsonConvert.SerializeObject(song);
            return Json(response);
        }
        public async Task<IActionResult> GetSongById(int id)
        {
            var song = await _musicService.GetSongById(id);
            if (song == null)
            {
                return NotFound();
            }
            string response = JsonConvert.SerializeObject(song);
            return Json(response);
        }
        public async Task<IActionResult> AddSong()
        {

            return Json("Success");
        }
        public async Task<IActionResult> UpdateSong()
        {

            return Json("Success");
        }
        public async Task<IActionResult> RemoveSong()
        {

            return Json("Success");
        }

        [HttpPost]
        public async Task<IActionResult> SaveSong(IFormFile file, string fileName)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("File is not provided or empty");
            }

            // Путь к директории сохранения файлов
            var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "songs");

            // Полный путь к файлу
            var filePath = Path.Combine(uploadsFolder, fileName);

            // Сохраняем файл на диск
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return Ok("File saved successfully");
        }


        [HttpPost]
        public async Task<IActionResult> SaveSongDbAsync([FromBody] VM_SongData songData)
        {
            try
            {
                // Создаем экземпляр песни
                T_Song song = new T_Song
                {
                    Title = songData.Title,
                    Poster = songData.Poster,
                    FilePath = songData.FilePath,
                    Duration = TimeSpan.FromSeconds(songData.Duration),
                    Likes = 0,
                    Plays = 0,
                    Downloads = 0,
                    UploadDate = DateOnly.FromDateTime(DateTime.Now),
                    DataUpdate = DateOnly.FromDateTime(DateTime.Now),
                };
                await _musicService.SaveSong(song, songData.ArtistIds, songData.AlbumIds);
                return Ok();
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        // ARTISTS
        public async Task<IActionResult> GetArtists()
        {
            var Artists = await _musicService.GetArtists();
            if (Artists == null)
            {
                return NotFound();
            }
            string response = JsonConvert.SerializeObject(Artists);
            return Json(response);
        }


        // ALBUMS
        public async Task<IActionResult> GetAlbums()
        {
            var Albums = await _musicService.GetAlbums();
            if (Albums == null)
            {
                return NotFound();
            }
            string response = JsonConvert.SerializeObject(Albums);
            return Json(response);
        }





        // GET: Management/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Management/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Management/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Management/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Management/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Management/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Management/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
