using MusicCollection_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicCollection_BLL.DTO
{
    public class T_Artist : T_DbEntity
    {
        public T_Artist() { }
        public T_Artist(M_Artist model)
        {
            Id = model.Id;
            UploadDate = model.UploadDate;
            DataUpdate = model.DataUpdate;

            Name = model.Name;
        }
        public string Name { get; set; }
    }
}
