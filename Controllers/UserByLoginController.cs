using Microsoft.AspNetCore.Mvc;
using WebForTest.Models;
using WebForTest.Services;

namespace WebForTest.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class UserByLoginController : Controller
    {
        private readonly UserService _userService;
        public UserByLoginController(UserService userService)
        {
            _userService = userService;
        }

        #region HttpGet
        [HttpPost("user-active")]
        public List<UserEntity> GetActive()
        {
            var users = _userService.GetActiveUsers();
            return users;
        }

        [HttpPost("user-current")]
        public UserEntity GetCurrentUser([FromBody] UserLogin userLogin)
        {
            _userService.Login(userLogin.Login, userLogin.Password);
            var result = _userService.GetCurrentUser();
            _userService.Logout();
            return result;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        [HttpPost("user-by-login")]
        public UserEntity GetByLogin([FromQuery] string login)
        {
            var userLogin = _userService.GetUserByLogin(login);
            return userLogin;
        }

        [HttpPost("user-by-birthday")]
        public List<UserEntity> GetByBirthday([FromQuery] int age)
        {
            var birthDay = _userService.GetUserByBirthday(age);
            return birthDay;
        }
        #endregion

        #region HttpPost

        [HttpPost("create-user")]
        public void CreateUser([FromBody] UserDetails userDetails)
        {
            _userService.CreateUser(userDetails);
        }

        #endregion

        #region HttpPut
        [HttpPut("change-user")]
        public void СhangeUser([FromBody] UserEntity user)
        {
            _userService.СhangeUser(user);
        }

        [HttpPut("change-password")]
        public void СhangePassword([FromQuery] string login, string password)
        {
            _userService.СhangePasswordUser(login, password);
        }

        [HttpPut("change-login")]
        public void СhangeLogin([FromQuery] string oldLogin, string newLogin)
        {
            _userService.СhangeLoginUser(oldLogin, newLogin);
        }

        [HttpPut("recovery-user")]
        public void RecoveryUser([FromQuery] string login)
        {
            _userService.RecoveryUser(login);
        }

        #endregion

        #region HttpDelete
        [HttpPost("delete-by-login")]
        public void DeleteByLogin([FromQuery] string login, bool softRemove)
        {
            _userService.DeleteUserByLogin(login, softRemove);
        }
        #endregion
    }
}
