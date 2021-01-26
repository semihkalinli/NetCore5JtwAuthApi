using Autoking.Task2.Core.Models;
using JwtBasicTwo.Core.Repositories;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Autoking.Task2.Business.Repository.Concrete
{
    public class UserRepository : IUserRepository
    {
        private readonly JwtBasicTwoContext _jwtBasicTwoContext;
        private User _user;
        private readonly AppSettings _appSettings;
        public UserRepository(JwtBasicTwoContext jwtBasicTwoContext, IOptions<AppSettings> appSettings)
        {
            _jwtBasicTwoContext = jwtBasicTwoContext;
            _appSettings = appSettings.Value;

        }


        public User Login(string userName, string password)
        {
            _user = _jwtBasicTwoContext.Users.Where(x => x.Username == userName && x.Password == password).FirstOrDefault();
            
            if (_user == null)
                return null;
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            _user.Refleshtoken = GenerateRefreshToken();

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, _user.Id.ToString()),
                    new Claim(ClaimTypes.Role, _user.Role)

                }),
                Expires = DateTime.UtcNow.AddMinutes(2),
              
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            _user.Token = tokenHandler.WriteToken(token);
            _user.Expiration = DateTime.Now.AddMinutes(5);
           // _user.Token = "ssss";
            //_jwtBasicTwoContext.Users.Add(new User { Username = "aaaa" });
            _jwtBasicTwoContext.SaveChanges();
            return _user;
        }

        //Reflesh token oluşturma işlemi
        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }
        public User RefleshTokenGet(string refleshToken)
        {
            var user = _jwtBasicTwoContext.Users.Where(x => x.Refleshtoken == refleshToken).FirstOrDefault();

            if (user == null)
                return null;
            if (user.Expiration > DateTime.Now)
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                    new Claim(ClaimTypes.Name, user.Id.ToString()),
                    new Claim(ClaimTypes.Role, user.Role)

                    }),
                    Expires = DateTime.UtcNow.AddMinutes(5),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                user.Token = tokenHandler.WriteToken(token);
                user.Refleshtoken = GenerateRefreshToken();
                user.Expiration = DateTime.Now.AddMinutes(5);
                _jwtBasicTwoContext.SaveChanges();
            }
            else
                return null;
            return user;
        }
        //İnsert işlemi yaparak db kullanıcı ekleme işlemi
        public string InsertUser(string name, string lastName, string password, string userName)
        {
            _jwtBasicTwoContext.Users.Add(new User { Firstname = name, Lastname = lastName, Username = userName, Password = password, Role = "user" });
            _jwtBasicTwoContext.SaveChanges();
            return "User kaydedildi";
        }
        // Dbdeki değerleri değiştirme işlemi
        public User UpdateUser(int id, string firstName, string lastName)
        {
            var user = _jwtBasicTwoContext.Users.Where(x => x.Id == id).FirstOrDefault();
            user.Firstname = firstName;
            user.Lastname = lastName;
            _jwtBasicTwoContext.SaveChanges();
            return user;
        }
        // gelen userName ile Dbden kullanıcı silme
        public string DeleteUser(string userName)
        {
            var user = _jwtBasicTwoContext.Users.Where(x => x.Username == userName).FirstOrDefault();
            _jwtBasicTwoContext.Users.Remove(user);
            _jwtBasicTwoContext.SaveChanges();
            return userName+" adlı kullanıcı silindi";
        }
        // Dbdeki User tablosunu listeleme
        public IEnumerable<User> GetAll()
        {
            return _jwtBasicTwoContext.Users.ToList();
        }
    }
}
