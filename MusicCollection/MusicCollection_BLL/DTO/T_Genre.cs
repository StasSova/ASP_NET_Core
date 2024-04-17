using MusicCollection_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicCollection_BLL.DTO
{
    public class T_Genre : T_DbEntity
    {
        public T_Genre() { }
        public T_Genre(M_Genre model)
        {
            Id = model.Id;
            UploadDate = model.UploadDate;
            DataUpdate = model.DataUpdate;

            Name = model.Name;
            Poster = model.Poster;
            BackgroundColor = model.BackgroundColor;
        }
        public string Name { get; set; }
        public string Poster { get; set; }
        public string BackgroundColor { get; set; }
    }
}
