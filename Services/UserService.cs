using WebForTest.Models;

namespace WebForTest.Services;

public class UserService
{
    private List<User> _users;
    public UserService()
    {
        _users = new List<User>();
    }

    #region Read(get)
    /// <summary>
    /// Запрос списка всех активных
    /// </summary>
    /// <returns></returns>
    public List<User> GetActiveUsers()
    {
        var resultRevokedOn = _users.Where(x => x.RevokedOn == null).OrderBy(x => x.CreatedOn).ToList();

        return resultRevokedOn;
    }

    /// <summary>
    /// Запрос пользователя по логину
    /// </summary>
    /// <param name="login"></param>
    /// <returns></returns>
    public User GetUserByLogin(string login)
    {
        var resultOnLogin = _users.FirstOrDefault(x => x.Login == login);

        return resultOnLogin;
    }

    /// <summary>
    /// Запрос пользователя по логину и паролю
    /// </summary>
    /// <param name="login"></param>
    /// <param name="pass"></param>
    /// <returns></returns>
    public User GetUserByLoginAndPass(string login, string pass)
    {
        var resultOnLogPass = _users.FirstOrDefault(x => x.Login == login && x.Password == pass);
        return resultOnLogPass;
    }

    /// <summary>
    /// Запрос всех пользователей старше определённого возраста ввод типа DateTime
    /// </summary>
    /// <param name="dayTime"></param>
    /// <returns></returns>
    public List<User> GetUserByBirthDay(DateTime dayTime)
    {
        var resultOnBirthDay = _users.Where(x => x.BirthDay > dayTime).ToList();

        return resultOnBirthDay;
    }
    /// <summary>
    /// Запрос всех пользователей старше определённого возраста ввод типа int
    /// </summary>
    /// <param name="age"></param>
    /// <returns></returns>
    public List<User> GetUserByBirthDay(int age)
    {
        var resultOnBirthDay = _users.Where(x => x.BirthDay > (DateTime.Today.AddYears(-age))).ToList();

        return resultOnBirthDay;
    }
    #endregion


    public User Create(User user)
    {
        _users.Add(user);

        return user;
    }

    public User Update(User user)
    {
        var resultUser = _users.FirstOrDefault(x => x.Guid == user.Guid);
        if(resultUser == null)
        {
            throw new Exception("Такого пользователя не существует");
        }
        _users.Remove(resultUser);
        _users.Add(user);
        
        return user;
    }
    /// <summary>
    /// Восстановление пользователя - Очистка полей (RevokedOn, RevokedBy)
    /// </summary>
    /// <param name="user"></param>
    public void RecoveryUser(User user)
    {
        var guidUser = _users.FirstOrDefault(x => x.Guid == user.Guid);
        
        if(guidUser != null)
        {
            _users.Remove(user);
            guidUser.RevokedOn = null;
            guidUser.RevokedBy = null;
            _users.Add(guidUser);
        }
    }

    public bool Delete(User user)
    {
        if(user == null)
        {
            return false;
        }

        return _users.Remove(user);
    }

    public void DeleteUserByLogin(string login)
    {
        var deleteUserOnLogin = _users.FirstOrDefault(x => x.Login == login);
        
        if(deleteUserOnLogin != null)
        {
            _users.Remove(deleteUserOnLogin);
        }
    }


}
