using Autoking.Task2.Core.Models;
using Autoking.Task2.Http.ViewModels;
using JwtBasicTwo.Core.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace JwtBasic.Controllers
{


    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        readonly JwtBasicTwoContext _context;
        private IUserRepository _userRepository;


        public UsersController(JwtBasicTwoContext content, IUserRepository userRepository)
        {
            _context = content;
            _userRepository = userRepository;
        }


        [HttpPost("login")]
        public IActionResult Authenticate([FromBody] LoginViewModel model)
        {
            var user = _userRepository.Login(model.Username, model.Password);

            if (user == null)
                return BadRequest(new { message = "Username veya password hatalı!" });

            return Ok(user);
        }

        [HttpPost("refreshToken")]
        public IActionResult Refresh(string refreshToken)
        {
            var user = _userRepository.RefleshTokenGet(refreshToken);

            if (user == null)
                return BadRequest(new { message = "Hatalı giriş yaptınız." });

            return Ok(user);
        }
    }
}
