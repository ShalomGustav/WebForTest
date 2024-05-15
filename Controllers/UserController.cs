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
        #region HttpGet
        [HttpGet("user-active")]
        public List<User> GetActive()
        {
            var users = _userService.GetActiveUsers();
            return users;
        }

        [HttpGet("user-by-login")]
        public User GetByLogin([FromQuery] string login)
        {
            var userLogin = _userService.GetUserByLogin(login);
            return userLogin;
        }

        [HttpGet("user-by-login-pass")]
        public User GetByLoginAndPass([FromQuery]string login,string pass)
        {
            var loginAndPass = _userService.GetUserByLoginAndPass(login, pass);
            return loginAndPass;
        }

        [HttpGet("user-by-birthday")]
        public List<User> GetByBirthDay([FromQuery]int age)
        {
            var birthDay = _userService.GetUserByBirthDay(age);
            return birthDay;
        }
        #endregion

        #region HttpPost

        [HttpPost("create-user-by-name-login-pass")]
        public void Create([FromBody] User user)
        {
            _userService.Create(user);
        }

        #endregion

        #region HttpPut
        [HttpPut("update")]
        public User UserUpdate([FromBody] User user)
        {
            return _userService.Update(user);
        }

        [HttpPut("change-name-gen-birthday")]
        public void СhangeUser([FromBody] User user)
        {
             _userService.СhangeNameGenBirthDay(user);
        }

        [HttpPut("change-password")]
        public void СhangeUserPassword([FromBody] User user)
        {
            _userService.СhangePassword(user);
        }

        [HttpPut("change-login")]
        public void СhangeUserLogin([FromBody] User user)
        {
            _userService.СhangeLogin(user);
        }

        [HttpPut("recovery-user")]
        public void RecoveryUser([FromBody] User user)
        {
            _userService.Recovery(user);
        }

        #endregion

        #region HttpDelete
        [HttpDelete("delete")]
        public void Delete([FromBody] User user)
        {
            _userService.Delete(user);
        }

        [HttpDelete("delete-by-login")]
        public void DeleteByLogin([FromQuery] string user)
        {
            _userService.DeleteUserByLogin(user);
        }
        #endregion
    }
}
