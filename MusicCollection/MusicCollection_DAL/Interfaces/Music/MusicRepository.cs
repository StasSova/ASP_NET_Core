using Microsoft.EntityFrameworkCore;
using MusicCollection_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicCollection_DAL.Interfaces.Music
{
    public class MusicRepository : IMusicRepository
    {
        private readonly SpotifyContext context;
        public MusicRepository(SpotifyContext context)
        {
            this.context = context;
        }
        public async Task<ICollection<M_Album>> GetPopularAlbumsByTimePeriod(DateOnly startDate, DateOnly endDate, int numberOfAlbums)
        {

            var popularAlbums = await context.Albums
                    .Where(a => a.DataUpdate >= endDate && a.DataUpdate <= startDate)
                    .OrderByDescending(a => a.Songs.Sum(s => s.Likes))
                    .Take(numberOfAlbums)
                    .ToListAsync();

            return popularAlbums;
        }
        public async Task<ICollection<M_Album>> GetPopularAlbumsByArtist(string artistName, int numberOfAlbums)
        {
            var artistAlbums = await context.Albums
                .Where(a => a.Artists.Any(ar => ar.Name == artistName))
                .OrderByDescending(a => a.Songs.Sum(s => s.Likes))
                .Take(numberOfAlbums)
                .ToListAsync();

            return artistAlbums;
        }
        public async Task<ICollection<M_Artist>> GetPopularArtistsByTimePeriod(DateOnly startDate, DateOnly endDate, int numberOfArtists)
        {
            var popularArtists = await context.Artists
                .Where(a => a.DataUpdate >= endDate && a.DataUpdate <= startDate)
                .OrderByDescending(a => a.Songs.Sum(s => s.Likes))
                .Take(numberOfArtists)
                .ToListAsync();

            return popularArtists;
        }

        public async Task<ICollection<M_Song>> GetSongsByAlbumId(int id)
        {
            try
            {
                var album = await context.Albums.FirstAsync(x => x.Id == id);
                return album.Songs;
            }
            catch
            {
                return null;
            }
        }

        public async Task<ICollection<M_Song>> GetSongsByArtistId(int id)
        {
            try
            {
                var album = await context.Artists.FirstAsync(x => x.Id == id);
                return album.Songs;
            }
            catch
            {
                return null;
            }
        }
    }
}
