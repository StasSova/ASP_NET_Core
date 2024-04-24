using Microsoft.EntityFrameworkCore;
using MusicCollection_BLL.DTO;
using MusicCollection_DAL.Interfaces;
using MusicCollection_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicCollection_BLL.Interfaces.Music
{
    public class MusicService : IMusicService
    {
        IUnitOfWork _dbContext;
        public MusicService(IUnitOfWork database)
        {
            _dbContext = database;
        }

        public async Task<ICollection<T_Album>> GetPopularAlbums()
        {
            try
            {
                var today = DateOnly.FromDateTime(DateTime.Now);
                DateOnly yesterdayDate = today.AddDays(-10);

                return (await _dbContext.Music
                        .GetPopularAlbumsByTimePeriod(today, yesterdayDate, 5))
                        .Select(x => new T_Album(x))
                        .ToList();
            }
            catch (Exception ex) { }
            return null;
        }

        public async Task<ICollection<T_Song>> GetSongsByAlbumId(int id)
        {
            try
            {
                return (await _dbContext.Music.GetSongsByAlbumId(id)).Select(x => new T_Song(x)).ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public async Task<ICollection<T_Song>> GetSongsByArtistId(int id)
        {
            try
            {
                return (await _dbContext.Music.GetSongsByArtistId(id)).Select(x => new T_Song(x)).ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        // SONGS

        public async Task<ICollection<T_Song>> GetSongs()
        {
            try
            {
                return (await _dbContext.Generic
                    .Get<M_Song>())
                    .Select(x => new T_Song(x))
                    .ToList();
            }
            catch { return null; }
        }
        public async Task<T_Song> GetSongById(int id)
        {
            try
            {
                var song = await _dbContext.Generic.GetFirstAsync<M_Song, int>("Id", id);
                return new T_Song(song);
            }
            catch { return null; }
        }
        public async Task SaveSong(T_Song song, ICollection<int> artists, ICollection<int> albums)
        {
            try
            {
                M_Song db = new M_Song()
                {
                    Title = song.Title,
                    Poster = song.Poster,
                    FilePath = song.FilePath,
                    Duration = song.Duration,
                    Likes = song.Likes,
                    Plays = song.Plays,
                    Downloads = song.Downloads,
                    UploadDate = song.UploadDate,
                    DataUpdate = song.DataUpdate,
                };
                // Устанавливаем альбом
                if (albums != null && albums.Any())
                {
                    db.AlbumId = albums.First();
                }

                // Добавляем существующих артистов к песне
                if (artists != null && artists.Any())
                {
                    foreach (var artistId in artists)
                    {
                        // Получаем сущность артиста по Id из базы данных
                        var existingArtist = await _dbContext.Generic.GetFirstAsync<M_Artist, int>("Id", artistId);
                        if (existingArtist != null)
                        {
                            // Добавляем артиста к песне
                            db.Artists ??= new List<M_Artist>();
                            db.Artists?.Add(existingArtist);
                        }
                    }
                }
                // добавляем альбомам песню
                foreach (var albumId in albums)
                {
                    var album = await _dbContext.Generic.GetFirstAsync<M_Album, int>("Id", albumId);
                    if (album != null)
                    {
                        // Проверяем, существует ли песня в альбоме
                        if (!album.Songs.Any(x => x.Id == db.Id))
                        {
                            album.Songs.Add(db);
                        }
                    }
                }

                // Добавляем песню в базу данных
                await _dbContext.Generic.Add<M_Song>(db);
                await _dbContext.SaveChangesAsync();
            }
            catch
            {

            }
        }

        // ARTISTS
        public async Task<ICollection<T_Artist>> GetArtists()
        {
            try
            {
                return (await _dbContext.Generic
                    .Get<M_Artist>())
                    .Select(x => new T_Artist(x))
                    .ToList();
            }
            catch { return null; }
        }
        public async Task<T_Artist> GetArtistById(int id)
        {
            try
            {
                var alb = await _dbContext.Generic.GetFirstAsync<M_Artist, int>("Id", id);
                return new T_Artist(alb);
            }
            catch
            {
                return null;
            }
        }

        // ALBUMS
        public async Task<ICollection<T_Album>> GetAlbums()
        {
            try
            {
                return (await _dbContext.Generic
                    .Get<M_Album>())
                    .Select(x => new T_Album(x))
                    .ToList();
            }
            catch { return null; }
        }
        public async Task<T_Album> GetAlbumById(int id)
        {
            try
            {
                var alb = await _dbContext.Generic.GetFirstAsync<M_Album, int>("Id", id);
                return new T_Album(alb);
            }
            catch
            {
                return null;
            }
        }
    }
}
