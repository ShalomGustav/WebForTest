using WebForTest.Models;

namespace WebForTest.Services;

public class UserService
{
    private UserEntity _currentUser;
    private List<UserEntity> _users;

    public UserService()
    {
        _currentUser = null;
        _users = new List<UserEntity>();

        ApplyDefaultUserData();
    }

    public string LoginToLower(string login)
    {
        return login.ToLower();
    }

    public void Login(string login, string password)
    {
        if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
        {
            throw new ArgumentNullException($"{nameof(login)} or {nameof(password)}");
        }

        var user = GetUserByLogin(login.ToLower());
        if (user == null)
        {
            throw new NullReferenceException("Не верный логин или пароль");
        }

        if (user.Password != password)
        {
            throw new ArgumentException("Не верный логин или пароль");
        }

        _currentUser = user;
    }

    public void Logout()
    {
        _currentUser = null;
    }

    public UserEntity GetCurrentUser()
    {
        return _currentUser;
    }

    /// <summary>
    /// Запрос списка всех активных
    /// </summary>
    /// <returns></returns>
    public List<UserEntity> GetActiveUsers()
    {
        var resultRevokedOn = _users.Where(x => x.RevokedOn == null).OrderBy(x => x.CreatedOn).ToList();

        return resultRevokedOn;
    }

    /// <summary>
    /// Получение по логину
    /// </summary>
    /// <param name="login"></param>
    /// <returns></returns>
    public UserEntity GetUserByLogin(string login)
    {
        var resultOnLogin = _users.FirstOrDefault(x => x.Login == login.ToLower());

        return resultOnLogin;
    }

    /// <summary>
    /// Запрос пользователя по логину и паролю
    /// </summary>
    /// <param name="login"></param>
    /// <param name="pass"></param>
    /// <returns></returns>
    public UserEntity GetUserByLoginAndPass(string login, string pass)
    {
        var resultOnLogPass = _users.FirstOrDefault(x => x.Login == login && x.Password == pass);
        return resultOnLogPass;
    }

    /// <summary>
    /// Запрос всех пользователей старше определённого возраста ввод типа int
    /// </summary>
    /// <param name="age"></param>
    /// <returns></returns>
    public List<UserEntity> GetUserByBirthday(int age)
    {
        var resultOnBirthDay = _users.Where(x => x.BirthDay > (DateTime.Today.AddYears(-age))).ToList();
        return resultOnBirthDay;
    }

    /// <summary>
    ///  Изменение имени, пола или даты рождения пользователя
    /// </summary>
    /// <param name="user"></param>
    /// <param name="updateUser"></param>
    public void СhangeUser(UserEntity user)
    {
        if (user == null)
        {
            throw new ArgumentNullException(nameof(user));
        }

        SaveUser(user);
    }

    /// <summary>
    /// Изменение пароля
    /// </summary>
    /// <param name="user"></param>
    /// <param name="updateUser"></param>
    public void СhangePasswordUser(string login, string password)
    {
        if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
        {
            throw new ArgumentNullException($"{nameof(login)} or {nameof(password)}");
        }

        var result = GetUserByLogin(login);

        if (result == null)
        {
            throw new NullReferenceException($"Пользователь с логином {login} не найден");
        }

        result.Password = password;
        SaveUser(result);
    }

    /// <summary>
    /// Изменение логина
    /// </summary>
    /// <param name="user"></param>
    /// <param name="updateUser"></param>
    public void СhangeLoginUser(string oldLogin, string newLogin)
    {
        if (string.IsNullOrEmpty(oldLogin) || string.IsNullOrEmpty(newLogin))
        {
            throw new ArgumentNullException($"{oldLogin} or {newLogin}");
        }
        var resultChangeUser = GetUserByLogin(oldLogin);

        if (resultChangeUser == null)
        {
            throw new NullReferenceException($"Пользователь с логином {oldLogin} не найден");
        }

        if (GetUserByLogin(newLogin) != null)
        {
            throw new Exception($"Пользователь с логином {newLogin} уже существует");
        }

        resultChangeUser.Login = newLogin;
        SaveUser(resultChangeUser);
    }

    /// <summary>
    /// Создание пользователя
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    public UserEntity CreateUser(UserDetails userDetails)
    {
        if(userDetails == null)
        {
            throw new ArgumentNullException(nameof(userDetails));
        }

        if (!IsAdmin())
        {
            throw new Exception("Недостаточно прав");
        }

        var user = UserEntity.ToUser(userDetails, _currentUser.Name);

        _users.Add(user);
        return user;
    }

    /// <summary>
    /// Удаление пользователя по логину
    /// </summary>
    /// <param name="login"></param>
    /// <param name="softRemove"></param>
    /// <exception cref="ArgumentNullException"></exception>
    /// <exception cref="NullReferenceException"></exception>
    /// <exception cref="Exception"></exception>
    public void DeleteUserByLogin(string login,bool softRemove = true)
    {
        if (string.IsNullOrEmpty(login))
        {
            throw new ArgumentNullException(nameof(login));
        }

        var deleteUserOnLogin = GetUserByLogin(login);

        if (deleteUserOnLogin == null)
        {
            throw new NullReferenceException($"Пользователь с логином {login} не найден");
        }

        if (!IsPossibleToChange())
        {
            throw new Exception("Недостаточно прав");
        }

        _users.Remove(deleteUserOnLogin);

        if (softRemove == true)
        {
            deleteUserOnLogin.RevokedOn = DateTime.Now;
            deleteUserOnLogin.RevokedBy = _currentUser.Name;
            deleteUserOnLogin.ModifiedOn = DateTime.Now;
            deleteUserOnLogin.ModifiedBy = _currentUser.Name;
            _users.Add(deleteUserOnLogin);
        }
    }

    /// <summary>
    /// Восстановление пользователя - Очистка полей (RevokedOn, RevokedBy)
    /// </summary>
    /// <param name="user"></param>
    public void RecoveryUser(string login)
    {
        if (string.IsNullOrEmpty(login))
        {
            throw new ArgumentNullException(nameof(login));
        }

        var recoveryUser = GetUserByLogin(login);

        if (recoveryUser == null)
        {
            throw new NullReferenceException($"Пользователь с логином {login} не найден");
        }

        if (!IsPossibleToChange())
        {
            throw new Exception("Недостаточно прав");
        }

        recoveryUser.RevokedOn = null;
        recoveryUser.RevokedBy = null;

        SaveUser(recoveryUser);
    }


    #region Private
    private void ApplyDefaultUserData()
    {
        var admin = new UserEntity()
        {
            Admin = true,
            Login = "admin",
            Password = "pass",
            Name = "Maxim",
        };
        _users.Add(admin);
    }

    private bool IsPossibleToChange()
    {
        if (_currentUser == null)
        {
            return false;
        }

        var result = IsAdmin();

        if (result == false)
        {
            result = _currentUser.RevokedOn == null;
        }

        return result;
    }

    private bool IsAdmin()
    {
        return _currentUser != null
            ? _currentUser.Admin
            : false;
    }

    private void SaveUser(UserEntity user)
    {
        var result = _users.FirstOrDefault(x => x.Guid == user.Guid);

        if (result == null)
        {
            throw new NullReferenceException($"Пользователя с логином {user.Login} не существует");
        }

        if (IsPossibleToChange())
        {
            user.ModifiedOn = DateTime.Now;
            user.ModifiedBy = _currentUser.Name;
            _users.Remove(result);
            _users.Add(user);
        }
    }
    #endregion
}
