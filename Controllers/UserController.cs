using Microsoft.AspNetCore.Identity;
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

        //[HttpGet("user-active")]
        //[ProducesResponseType(typeof(User),200)]
        //public IActionResult GetActive()
        //{
        //    var users = _userService.GetActiveUsers();
        //    return Ok(users);
        //}

        [HttpGet("user-active")]
        public List<User> GetActive()
        {
            var users = _userService.GetActiveUsers();
            return users;
        }

        [HttpGet("user-by-login")]
        public User GetByLogin(string login)
        {
            var userLogin = _userService.GetUserByLogin(login);
            return userLogin;
        }

        [HttpGet("user-by-login-pass")]
        public User GetByLoginAndPass(string login,string pass)
        {
            var loginAndPass = _userService.GetUserByLoginAndPass(login, pass);
            return loginAndPass;
        }

        [HttpGet("user-by-birthday")]
        public List<User> GetByBirthDay(int age)
        {
            var birthDay = _userService.GetUserByBirthDay(age);
            return birthDay;
        }

        [HttpPost("create-user-by-name-login-pass")]
        public void Create(User user)
        {
            _userService.Create(user);
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
