using Microsoft.Extensions.DependencyInjection;
using MusicCollection_BLL.Interfaces.User;
using MusicCollection_DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicCollection_BLL.ServiceExtensions
{
    public static class UserAuthenticationExtensions
    {
        public static void AddUserAuthentication(this IServiceCollection services)
        {
            services.AddScoped<IUserAuthentication, UserAuthentication>();
        }
    }
}
