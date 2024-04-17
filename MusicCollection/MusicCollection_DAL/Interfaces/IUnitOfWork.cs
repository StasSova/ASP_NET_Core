using MusicCollection_DAL.Interfaces.Generic;
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
    }
}
