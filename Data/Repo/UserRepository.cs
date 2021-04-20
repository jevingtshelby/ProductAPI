using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ProductAPI.Controllers.Resources;
using ProductAPI.Interfaces;
using ProductAPI.Models;
using ProductApp.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace ProductAPI.Data.Repo
{
    public class UserRepository : IUserRepository
    {
        private IDbConnection db;

        public UserRepository(IConfiguration configuration)
        {
            this.db = new SqlConnection(configuration.GetConnectionString("ProductAppCon"));
        }

        public async Task<User> Authenticate(string userName, string passwordText)
        {
            //return await dc.Users.FirstOrDefaultAsync(x => x.UserName == userName && x.Password == password);

            var sql = "SELECT * FROM Users WHERE UserName=@UserName";
            var userResult = await db.QueryAsync<User>(sql, new { @UserName = userName });

            var user = userResult.FirstOrDefault();

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
            user.IsActive = true;

            var sql = "INSERT INTO Users(UserName,Firstname,Lastname,IsActive,Email,DateOfBirth,Password,PasswordKey) VALUES(@UserName,@Firstname,@Lastname,@IsActive,@Email,@DateOfBirth,@Password,@PasswordKey)";
            db.Execute(sql, user);


        }

        public async Task<bool> UserAlreadyExists(string userName)
        {
            //return await dc.Users.AnyAsync(x => x.UserName == userName);

            var sql = "SELECT * FROM Users WHERE UserName=@UserName";
            var user = await db.QueryAsync<User>(sql, new { @UserName = userName });

            return user.Any();
        }

        //public async Task<int> SaveChanges()
        //{
        //    return await dc.SaveChangesAsync();
        //}
    }
}
