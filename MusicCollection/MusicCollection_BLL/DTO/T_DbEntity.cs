using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicCollection_BLL.DTO
{
    public class T_DbEntity
    {
        public int Id { get; set; }
        public DateOnly UploadDate { get; set; }
        public DateOnly DataUpdate { get; set; }
    }
}
