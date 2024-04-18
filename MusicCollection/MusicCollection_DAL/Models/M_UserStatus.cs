using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicCollection_DAL.Models
{
    public class M_UserStatus : M_DbEntity
    {
        public required string Status { get; set; }
        public virtual ICollection<M_User>? Users { get; set; }
    }
}
