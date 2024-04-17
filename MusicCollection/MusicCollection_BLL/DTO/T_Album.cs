using MusicCollection_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicCollection_BLL.DTO
{
    public class T_Album : T_DbEntity
    {
        public T_Album() { }
        public T_Album(M_Album model)
        {
            Id = model.Id;
            UploadDate = model.UploadDate;
            DataUpdate = model.DataUpdate;

            Title = model.Title;
            Poster = model.Poster;
            Artists = model.Artists?.Select(x => x.Name).ToList();
            Songs = model.Songs?.Select(x => x.Title).ToList();
        }
        public required string Title { get; set; }
        public string Poster { get; set; }
        public List<string>? Artists { get; set; } = new List<string>();
        public List<string>? Songs { get; set; } = new List<string>();
    }
}
