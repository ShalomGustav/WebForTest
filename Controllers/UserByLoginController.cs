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

        [HttpPost("active-users")]
        public List<UserEntity> GetActive([FromBody] UserLogin userLogin)
        {
            _userService.Login(userLogin.Login, userLogin.Password);
            var users = _userService.GetActiveUsers();
            _userService.Logout();
            return users;
        }

        [HttpPost("current-user")]
        public UserEntity GetCurrentUser([FromBody] UserLogin userLogin)
        {
            _userService.Login(userLogin.Login, userLogin.Password);
            var result = _userService.GetCurrentUser();
            _userService.Logout();
            return result;
        }

        [HttpPost("user-by-login/{login}")]
        public UserEntity GetByLogin(string login, [FromBody] UserLogin userLogin)
        {
            _userService.Login(userLogin.Login, userLogin.Password);
            var result = _userService.GetUserByLogin(login);
            _userService.Logout();
            return result;
        }

        [HttpPost("user-by-birthday")]
        public List<UserEntity> GetByBirthday([FromQuery] int age, [FromBody] UserLogin userLogin)
        {
            _userService.Login(userLogin.Login, userLogin.Password);
            var birthDay = _userService.GetUserByBirthday(age);
            _userService.Logout();
            return birthDay;
        }

        [HttpPost("create-user")]
        public void CreateUser([FromQuery] UserDetails userDetails, [FromBody] UserLogin user)
        {
            _userService.Login(user.Login, user.Password);
            _userService.CreateUser(userDetails);
            _userService.Logout();
        }

        [HttpPut("update-user")]
        public void UpdateUser([FromQuery] UserEntity user, [FromBody] UserLogin userLogin)
        {
            _userService.Login(userLogin.Login, userLogin.Password);
            _userService.UpdateUser(user);
            _userService.Logout();
        }

        [HttpPut("change-password")]
        public void СhangePassword([FromQuery] string login, string password, [FromBody] UserLogin userLogin)
        {
            _userService.Login(userLogin.Login, userLogin.Password);
            _userService.UpdatePasswordUser(login, password);
            _userService.Logout();
        }

        [HttpPut("change-login")]
        public void СhangeLogin([FromQuery] string oldLogin, string newLogin, [FromBody] UserLogin userLogin)
        {
            _userService.Login(userLogin.Login, userLogin.Password);
            _userService.UpdateLoginUser(oldLogin, newLogin);
            _userService.Logout();
        }

        [HttpPost("delete-by-login/{login}")]
        public void DeleteByLogin(string login, bool softRemove, [FromBody] UserLogin userLogin)
        {
            _userService.Login(userLogin.Login, userLogin.Password);
            _userService.DeleteUserByLogin(login, softRemove);
            _userService.Logout();
        }

        [HttpPut("recovery-user")]
        public void RecoveryUser([FromQuery] string login, [FromBody] UserLogin userLogin)
        {
            _userService.Login(userLogin.Login, userLogin.Password);
            _userService.RecoveryUser(login);
            _userService.Logout();
        }
    }
}
