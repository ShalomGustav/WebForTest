using Microsoft.AspNetCore.Mvc;
using WebForTest.Models;
using WebForTest.Services;

namespace WebForTest.Controllers;

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
    public List<UserEntity> GetActive()
    {
        var users = _userService.GetActiveUsers();
        return users;
    }

    [HttpGet("user-current")]
    public UserEntity GetCurrentUser()
    {
        var result = _userService.GetCurrentUser();
        return result;
    }

    [HttpGet("logout")]
    public ActionResult Logout()
    {
        _userService.Logout();
        return Ok();
    }

    [HttpGet("user-by-login")]
    public UserEntity GetByLogin([FromQuery] string login)
    {
        var userLogin = _userService.GetUserByLogin(login);
        return userLogin;
    }

    [HttpGet("login")]
    public ActionResult Login([FromQuery] string login,string pass)
    {
        _userService.Login(login, pass);
        return Ok();
    }

    [HttpGet("user-by-birthday")]
    public List<UserEntity> GetByBirthday([FromQuery] int age)
    {
        var birthDay = _userService.GetUserByBirthday(age);
        return birthDay;
    }

    [HttpPost("create-user")]
    public void CreateUser([FromBody] UserDetails userDetails)
    {
        _userService.CreateUser(userDetails);
    }

    [HttpPut("change-user")]
    public void СhangeUser([FromBody] UserEntity user)
    {
         _userService.СhangeUser(user);
    }

    [HttpPut("change-password")]
    public void СhangePassword([FromQuery] string login, string password)
    {
        _userService.СhangePasswordUser(login,password);
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

    [HttpDelete("delete-by-login")]
    public void DeleteByLogin([FromQuery] string login,bool softRemove)
    {
        _userService.DeleteUserByLogin(login,softRemove);
    }
}
