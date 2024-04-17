using MusicCollection_DAL.Interfaces.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicCollection_DAL.Interfaces
{
    public class EFUnitOfWork : IUnitOfWork
    {
        private GenericRepository generic;
        private SpotifyContext spotifyContext;
        public EFUnitOfWork(SpotifyContext context)
        {
            spotifyContext = context;
        }
        public IGeneric Generic
        {
            get
            {
                return generic ??= new GenericRepository(spotifyContext);
            }
        }
    }
}
