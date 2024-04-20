using MusicCollection_BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicCollection_BLL.Interfaces.Music
{
    public interface IMusicService
    {
        Task<T_Album> GetAlbumById(int id);
        Task<ICollection<T_Album>> GetPopularAlbums();
        Task<ICollection<T_Song>> GetSongsByAlbumId(int id);
    }
}
