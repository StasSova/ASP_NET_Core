using Microsoft.Extensions.DependencyInjection;
using MusicCollection_DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicCollection_BLL.ServiceExtensions
{
    public static class PasswordHashingExtensions
    {
        public static void AddPasswordHashing(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, EFUnitOfWork>();
        }
    }
}
