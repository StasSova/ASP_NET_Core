using MusicCollection_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicCollection_BLL.DTO
{
    public class T_Song : T_DbEntity
    {
        public T_Song() { }
        public T_Song(M_Song model)
        {
            Id = model.Id;
            UploadDate = model.UploadDate;
            DataUpdate = model.DataUpdate;

            Title = model.Title;
            Poster = model.Poster;
            FilePath = model.FilePath;
            Duration = model.Duration;
            Likes = model.Likes;
            Plays = model.Plays;
            Downloads = model.Downloads;
            AlbumId = model.AlbumId;
            Album = model.Album?.Title;
        }

        public string Title { get; set; }
        public string Poster { get; set; }
        public string FilePath { get; set; }
        public TimeSpan Duration { get; set; }
        public int Likes { get; set; }
        public int Plays { get; set; }
        public int Downloads { get; set; }
        public int AlbumId { get; set; }
        public string? Album { get; set; }
    }
}
