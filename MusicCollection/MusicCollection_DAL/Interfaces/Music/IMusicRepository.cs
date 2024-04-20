using MusicCollection_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicCollection_DAL.Interfaces.Music
{
    public interface IMusicRepository
    {
        Task<ICollection<M_Album>> GetPopularAlbumsByArtist(string artistName, int numberOfAlbums);
        Task<ICollection<M_Album>> GetPopularAlbumsByTimePeriod(DateOnly startDate, DateOnly endDate, int numberOfAlbums);
        Task<ICollection<M_Song>> GetSongsByAlbumId(int id);
    }
}
