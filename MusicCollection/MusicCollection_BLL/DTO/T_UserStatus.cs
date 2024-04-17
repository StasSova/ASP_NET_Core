using MusicCollection_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicCollection_BLL.DTO
{
    public class T_UserStatus : T_DbEntity
    {
        public T_UserStatus() { }
        public T_UserStatus(M_UserStatus model)
        {
            Id = model.Id;
            UploadDate = model.UploadDate;
            DataUpdate = model.DataUpdate;

            Status = model.Status;
        }
        public required string Status { get; set; }
    }
}
