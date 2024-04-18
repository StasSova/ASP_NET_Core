using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MusicCollection_DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicCollection_BLL.ServiceExtensions
{
    public static class SpotifyContextExtensions
    {
        public static void AddSpotifyContext(this IServiceCollection services, string connection)
        {
            services.AddDbContext<SpotifyContext>(options => options.UseSqlServer(connection).UseLazyLoadingProxies());
        }
    }
}
