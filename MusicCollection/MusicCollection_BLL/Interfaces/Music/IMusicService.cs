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
        Task DeleteSongById(int id);
        Task<T_Album> GetAlbumById(int id);
        Task<ICollection<T_Album>> GetAlbums();
        Task<T_Artist> GetArtistById(int id);
        Task<ICollection<T_Artist>> GetArtists();
        Task<ICollection<T_Album>> GetPopularAlbums();
        Task<T_Song> GetSongById(int id);
        Task<ICollection<T_Song>> GetSongs();
        Task<ICollection<T_Song>> GetSongsByAlbumId(int id);
        Task<ICollection<T_Song>> GetSongsByArtistId(int id);
        Task SaveSong(T_Song song, ICollection<int> artists, ICollection<int> albums);
        Task UpdateSong(int id, T_Song song, ICollection<int> artists, ICollection<int> albums);
    }
}
