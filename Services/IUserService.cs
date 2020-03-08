using SteamingService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SteamingService.Services
{
    public interface IUserService
    {
        public User Authenticate(string username, string password);
        public User Register(User user, string password);
        public User GetUser(string username);
    }
}
