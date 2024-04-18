using Microsoft.Extensions.DependencyInjection;
using MusicCollection_BLL.Interfaces.Password;
using MusicCollection_DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicCollection_BLL.ServiceExtensions
{
    public static class UnitOfWorkExtensions
    {
        public static void AddUnitOfWork(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, EFUnitOfWork>();
        }
    }
}
