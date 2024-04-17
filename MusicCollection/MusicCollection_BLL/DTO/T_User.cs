using MusicCollection_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicCollection_BLL.DTO
{
    public class T_User : T_DbEntity
    {
        public T_User() { }
        public T_User(M_User model)
        {
            Id = model.Id;
            UploadDate = model.UploadDate;
            DataUpdate = model.DataUpdate;

            Email = model.Email;
            Password = model.Password;
            Salt = model.Salt;

            StatusId = model.StatusId;
            Status = model.Status?.Status;
        }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }

        public int StatusId { get; set; }
        public string? Status { get; set; }
    }
}
