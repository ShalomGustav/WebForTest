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
        [ProducesResponseType(typeof(List<User>), 200)]
        public IActionResult Read()
        {
            var users = _userService.GetActiveUsers();
            return (IActionResult)users;
        }

        [HttpGet("user-by-login")]
        [ProducesResponseType(typeof(User), 200)]
        public IActionResult ReadUserByLogin(string login)
        {
            var userLogin = _userService.GetUserByLogin(login);
            return (IActionResult)userLogin;
        }

        [HttpGet("user-by-login-pass")]
        [ProducesResponseType(typeof(User),200)]
        public IActionResult ReadUserByLoginAndPass(string login,string pass)
        {
            var logAndPass = _userService.GetUserByLoginAndPass(login, pass);
            return (IActionResult)logAndPass;
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
