using Microsoft.EntityFrameworkCore;
using ProductAPI.Interfaces;
using ProductAPI.Models;
using ProductApp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductAPI.Data.Repo
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext dc;

        public UserRepository(ApplicationDbContext dc)
        {
            this.dc = dc;
        }

        public async Task<User> Authenticate(string userName, string password)
        {
            return await dc.Users.FirstOrDefaultAsync(x => x.UserName == userName && x.Password == password);
        }
    }
}
