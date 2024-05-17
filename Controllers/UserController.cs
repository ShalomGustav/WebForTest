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
        public void Create([FromQuery] User user)
        {
            _userService.CreateUser(user);
        }

        #endregion

        #region HttpPut
        [HttpPut("update")]
        public User Update([FromQuery] User user)
        {
            return _userService.UpdateUser(user);
        }

        [HttpPut("change-name-gen-birthday")]
        public void СhangeNameGenBirthDay([FromQuery] string login, User user)
        {
             _userService.СhangeUserNameGenBirthDay(login,user);
        }

        [HttpPut("change-password")]
        public void СhangePassword([FromQuery] string login, string password)
        {
            _userService.СhangePasswordUser(login,password);
        }

        [HttpPut("change-login")]
        public void СhangeUser([FromQuery] string login)
        {
            _userService.СhangeLoginUser(login);
        }

        [HttpPut("recovery-user")]
        public void Recovery([FromQuery] string login)
        {
            _userService.RecoveryUser(login);
        }

        #endregion

        #region HttpDelete
        [HttpDelete("delete-by-login")]
        public void DeleteByLogin([FromQuery] string login,bool softRemove)
        {
            _userService.DeleteUserByLogin(login,softRemove);
        }
        #endregion
    }
}
