using MusicCollection_DAL.Interfaces.Generic;
using MusicCollection_DAL.Interfaces.Music;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicCollection_DAL.Interfaces
{
    public interface IUnitOfWork
    {
        IGeneric Generic { get; }
        IMusicRepository Music { get; }
    }
}
