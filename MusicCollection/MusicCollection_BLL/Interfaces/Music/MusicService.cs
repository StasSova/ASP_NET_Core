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
    }
}
