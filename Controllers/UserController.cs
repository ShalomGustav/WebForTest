using Microsoft.AspNetCore.Mvc;
using WebForTest.Models;
using WebForTest.Services;

namespace WebForTest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        [HttpGet("user-active")]
        [ProducesResponseType(200, Type = typeof(List<User>))]
        public IActionResult Read()
        {
            var users = _userService.GetActiveUsers();
            return (IActionResult)users;
        }

        [HttpGet("user-by-login")]
        [ProducesResponseType(200, Type = typeof(User))]
        public IActionResult ReadUserByLogin(string login)
        {
            if(login == null)
            {
                return BadRequest();
            }
            var userLogin = _userService.GetUserByLogin(login);
            return (IActionResult)userLogin;
        }

        [HttpGet("user-by-login-pass")]
        [ProducesResponseType(200, Type = typeof(User))]
        public IActionResult ReadUserByLoginAndPass(string login,string pass)
        {
            var loginAndPass = _userService.GetUserByLoginAndPass(login, pass);
            return (IActionResult)loginAndPass;
        }

        [HttpPost]
        public void Create()
        {

        }

        [HttpPut]
        public void Update() 
        { 

        }

        [HttpDelete]
        public void Delete() 
        {

        }

        
    }
}
