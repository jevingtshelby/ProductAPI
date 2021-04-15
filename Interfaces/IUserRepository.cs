using ProductAPI.Controllers.Resources;
using ProductAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductAPI.Interfaces
{
    public interface IUserRepository
    {
        Task<User> Authenticate(string userName, string password);
        void Register(LoginRequestResource registerReq);

        Task<bool> UserAlreadyExists(string userName);
        Task<int> SaveChanges();
    }
}
