using Microsoft.EntityFrameworkCore;
using ProductAPI.Controllers.Resources;
using ProductAPI.Interfaces;
using ProductAPI.Models;
using ProductApp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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

        public async Task<User> Authenticate(string userName, string passwordText)
        {
            //return await dc.Users.FirstOrDefaultAsync(x => x.UserName == userName && x.Password == password);
            var user = await dc.Users.FirstOrDefaultAsync(x => x.UserName == userName);

            if (user == null)
                return null;

            if(!MatchPasswordHash(passwordText, user.Password, user.PasswordKey))
            {
                return null;
            }

            return user;
        }

        private bool MatchPasswordHash(string passwordText, byte[] password, byte[] passwordKey)
        {
            using (var hmac = new HMACSHA512(passwordKey))
            {
                var passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(passwordText));
            
                for(int i=0; i < passwordHash.Length; i++)
                {
                    if (passwordHash[i] != password[i])
                        return false;
                }

                return true;
            }
        }

        public void Register(RegisterRequestResource registerReq)
        {
            byte[] passwordHash, passwordKey;

            using(var hmac = new HMACSHA512())
            {
                passwordKey = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(registerReq.Password));
            }

            User user = new User();
            user.UserName = registerReq.UserName;
            user.Password = passwordHash;
            user.PasswordKey = passwordKey;
            user.Firstname = registerReq.Firstname;
            user.Lastname = registerReq.Lastname;
            user.Email = registerReq.Email;
            user.DateOfBirth = registerReq.DateOfBirth;

            dc.Users.Add(user);
             
        }

        public async Task<bool> UserAlreadyExists(string userName)
        {
            return await dc.Users.AnyAsync(x => x.UserName == userName);
        
        }

        public async Task<int> SaveChanges()
        {
            return await dc.SaveChangesAsync();
        }
    }
}
