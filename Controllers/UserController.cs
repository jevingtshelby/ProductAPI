using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.Configuration;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ProductAPI.Controllers.Resources;
using ProductAPI.Interfaces;
using ProductAPI.Models;
using ProductApp.Data;
using Microsoft.Extensions.Configuration;

namespace ProductAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
       
        private readonly IUserRepository userRepo;
        private readonly Microsoft.Extensions.Configuration.IConfiguration configuration;

        public UserController(IUserRepository userRepo, Microsoft.Extensions.Configuration.IConfiguration configuration)
        {
      
            this.userRepo = userRepo;
            this.configuration = configuration;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequestResource loginReq)
        {
            var user = await userRepo.Authenticate(loginReq.UserName, loginReq.Password);

            if (user == null)
                return Unauthorized();

            var loginRes = new LoginResponseResource();
            loginRes.UserName = user.UserName;
            loginRes.Token = CreateJWT(user);

            return Ok(loginRes);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(LoginRequestResource registerReq)
        {
            if (await userRepo.UserAlreadyExists(registerReq.UserName))
                return BadRequest("User already exists, please try something else");

            userRepo.Register(registerReq);

            await userRepo.SaveChanges();

            return StatusCode(201);
        }

        public string CreateJWT(User user)
        {
            var secretKey = configuration.GetSection("AppSettings:Key").Value;
            var key = new SymmetricSecurityKey(Encoding.UTF8
                .GetBytes(secretKey));

            var claims = new Claim[]
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
            };

            var signingCredentials = new SigningCredentials(
                key, SecurityAlgorithms.HmacSha256Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(10),
                SigningCredentials = signingCredentials
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}