﻿using WebForTest.Models;

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
    /// 
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

    
    //public List<User> GetUserByBirthDay(DateTime dayTime)
    //{
    //    var resultOnBirthDay = _users.Where(x => x.BirthDay > dayTime).ToList();

    //    return resultOnBirthDay;
    //}

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

    #region Create(post)

    /// <summary>
    /// Создание пользователя
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    public User Create(User user)
    {
        if(user == null)
        {
            throw new Exception();
        }

        _users.Add(user);
        return user;
    }

    #endregion

    #region Update(put)

    /// <summary>
    /// 
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public User Update(User user)
    {
        var resultUser = _users.FirstOrDefault(x => x.Guid == user.Guid);
        if (resultUser == null)
        {
            throw new Exception("Такого пользователя не существует");
        }
        _users.Remove(resultUser);
        _users.Add(user);

        return user;
    }

    /// <summary>
    ///  Изменение имени, пола или даты рождения пользователя
    /// </summary>
    /// <param name="user"></param>
    /// <param name="updateUser"></param>
    public void СhangeNameGenBirthDay(User updateUser)
    {
        if(updateUser == null)
        {
            throw new Exception();
        }
        else
        {
            var userOld = _users.FirstOrDefault(x => x.Guid == updateUser.Guid && updateUser.RevokedOn == null);
            if (userOld == null)
            {
                throw new Exception();
            }
            else
            {
                userOld.Name = updateUser.Name;
                userOld.Gender = updateUser.Gender;
                userOld.BirthDay = updateUser.BirthDay;
            }
        }
    }

    /// <summary>
    /// Изменение пароля
    /// </summary>
    /// <param name="user"></param>
    /// <param name="updateUser"></param>
    public void СhangePassword(User user)
    {
        if(user == null)
        {
            throw new Exception();
        }
        else
        {
            var resultCreateUser = GetUserByLogin(user);
            var resultUpdateUser = _users.FirstOrDefault(x => x.Guid == user.Guid && user.RevokedOn == null);
            if (resultUpdateUser == null)
            {
                resultUpdateUser.Password = user.Password;
            }
            else
            {
                throw new Exception();
            }
        }
    }

    /// <summary>
    /// Изменение логина
    /// </summary>
    /// <param name="user"></param>
    /// <param name="updateUser"></param>
    public void СhangeLogin(string login)
    {
        if(login == null)
        {
            throw new Exception();
        }
        var resultChangeUser = GetUserByLogin(login);
        //var resultUpdateUser = _users.FirstOrDefault(x => x.Guid == login.Guid && x.RevokedOn == null);

        if (resultChangeUser == null)
        {
            throw new Exception();
        }
        else
        {
            if(resultChangeUser.RevokedOn == null)
            {
                if (!resultChangeUser.Login.Contains(resultChangeUser.Login))
                {
                    resultChangeUser.Login = login;
                }
                else
                {
                    throw new Exception("Такой логин существует");
                }
            }
        }
    }

    /// <summary>
    /// Восстановление пользователя - Очистка полей (RevokedOn, RevokedBy)
    /// </summary>
    /// <param name="user"></param>
    public void RecoveryUser(string login)
    {
        if(login == null)
        {
            throw new Exception();
        }
        else
        {
            var recoveryUser = GetUserByLogin(login);

            if (recoveryUser != null)
            {
                _users.Remove(recoveryUser);
                recoveryUser.RevokedOn = null;
                recoveryUser.RevokedBy = null;
                _users.Add(recoveryUser);
            }
        }
    }
    #endregion

    #region Delete(delete)
 
    public void DeleteUserByLogin(string login,bool softRemove = true)
    {
        if (login == null)
        {
            throw new Exception();
        }
        else
        {
            var deleteUserOnLogin = GetUserByLogin(login);

            if (softRemove == true)
            {
                _users.Remove(deleteUserOnLogin);
                deleteUserOnLogin.RevokedBy = null;
                deleteUserOnLogin.RevokedBy = null;
                _users.Add(deleteUserOnLogin);
            }
            else
            {
                _users.Remove(deleteUserOnLogin);
            }
        }
    }
    #endregion
}
