﻿using Business.Abstract;
using Core.Entities.Concrete;
using Entities.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace E2PProductBankAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        IUserService _userService;
        private readonly IConfiguration _configuration;
        public AuthController(IConfiguration configuration, IUserService userService)
        {
            _userService = userService;
            _configuration = configuration;
        }

        [HttpPost]
        [Route("register")]
        public IActionResult Register(UserRegisterDto request)
        {
            CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);
            var user = new User
            {
                Email = request.Email,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Firstname = request.Firstname,
                Lastname = request.Lastname,
                Username = request.Username,
                Country = request.Country,
                Phone = request.Phone,
                Status = request.Status
            };
            var result = _userService.Add(user);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }


        [HttpPost]
        [Route("login")]
        public IActionResult Login(UserLoginDto request)
        {
            var userToCheck = _userService.GetByMail(request.Email);
            string message = "";
            if (userToCheck == null)
            {
                return BadRequest("User not found");
            }

            if(!VerifyPasswordHash(request.Password, userToCheck.PasswordHash, userToCheck.PasswordSalt))
            {
                message = "Kullanıcı adınız veya şifreniz yanlış.";
                return BadRequest(message);
            }

            string token = CreateToken(userToCheck);
            
            return Ok(new { userToCheck, token });
        }

        [HttpGet]
        public User GetById(int userId)
        {
            var result = _userService.GetById(userId);
            return result;
        }
        
        [HttpGet]
        [Route("/comments/{userId}")]
        public IActionResult GetCommentsUser(int userId)
        {
            var result = _userService.GetCommentsUser(userId);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet]
        [Route("/likes/{userId}")]
        public IActionResult GetLikesUser(int userId)
        {
            var result = _userService.GetLikesUser(userId);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Firstname + " " + user.Lastname),
                new Claim(ClaimTypes.Role, "User")
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("TokenOptions:SecurityKey").Value));

            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                    claims: claims,
                    expires: DateTime.Now.AddDays(1),
                    signingCredentials: cred
                );
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                //var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                //for (int i = 0; i < computedHash.Length; i++)
                //{
                //    if (computedHash[i] != passwordHash[i])
                //    {
                //        return false;
                //    }
                //}
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }
    }
}
