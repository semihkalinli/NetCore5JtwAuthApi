using Autoking.Task2.Core.Models;
using JwtBasicTwo.Core.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Security.Claims;

namespace JwtBasic.Controllers
{

    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class SecondController : ControllerBase
    {
        readonly JwtBasicTwoContext _context;
        private IUserRepository _userRepository;

        public SecondController(JwtBasicTwoContext content, IUserRepository userRepository)
        {
            _context = content;
            _userRepository = userRepository;
        }
        // Rolü admin olan kullanıcının yeni kullanıcı kayıt etmesi
        [Authorize]
        [HttpGet("insert")]
        public IActionResult SecondControllerInsert(string name, string lastName, string password, string userName)
        {
            var result = _userRepository.InsertUser(name, lastName, password, userName);
            return Ok(result);
        }
        // Authorize olmuş kullanıcının adı ve soyadını değiştirmesi
        [Authorize]
        [HttpGet("update")]
        public IActionResult SecondControllerUpdate(string firstName, string lastName)
        {
            var xx = ((ClaimsIdentity)User.Identity).Claims
                 .Where(c => c.Type == ClaimTypes.Name)
                 .Select(c => c.Value).FirstOrDefault();
            var UserId = Convert.ToInt32(xx);
            var result = _userRepository.UpdateUser(UserId, firstName, lastName);
            return Ok(result);
        }
        // Rolü admin olan kullanıcının user silme işlemi
        [Authorize]
        [HttpGet("delete")]
        public IActionResult SecondControllerDelete(string userName)
        {
            var result = _userRepository.DeleteUser(userName);
            return Ok(result);
        }
        // Rolü admin olan kullanıcı tüm userları listeleyebilir
        [Authorize(Roles = Role.Admin)]
        [HttpGet("list")]
        public IActionResult SecondControllerList()
        {
            var users = _userRepository.GetAll();
            return Ok(users);
        }
    }
}
