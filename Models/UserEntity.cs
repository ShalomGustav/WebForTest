namespace WebForTest.Models;

public class UserEntity
{
    /// <summary>
    /// Уникальный идентификатор пользователя
    /// </summary>
    public Guid Guid { get; set; }
    
    /// <summary>
    /// Дата создания пользователя
    /// </summary>
    public DateTime CreatedOn { get; set; }
    /// <summary>
    /// Логин пользователя, от имени которого этот пользовател  создан
    /// </summary>
    public string CreatedBy { get; set; }
    
    /// <summary>
    /// Дата изменения пользователя
    /// </summary>
    public DateTime ModifiedOn { get; set; }
    /// <summary>
    /// Логин пользователя, от имени которого этот пользователь изменён
    /// </summary>
    public string ModifiedBy { get; set;}
    
    /// <summary>
    /// Дата удаления пользователя
    /// </summary>
    public DateTime? RevokedOn { get; set; }
    /// <summary>
    /// Логин пользователя, от имени которого этот пользователь удалён
    /// </summary>
    public string RevokedBy { get; set;}

    /// <summary>
    /// Уникальный Логин (запрещены все символы кроме латинских букв и цифр),
    /// </summary>
    public string Login { get; set; }
    /// <summary>
    /// Пароль(запрещены все символы кроме латинских букв и цифр)
    /// </summary>
    public string Password { get; set; }
    /// <summary>
    /// Имя (запрещены все символы кроме латинских и русских букв)
    /// </summary>
    public string Name { get; set; }
    /// <summary>
    /// Пол 0 - женщина, 1 - мужчина, 2 - неизвестно
    /// </summary>
    public int Gender { get; set; }
    /// <summary>
    /// поле даты рождения может быть Null
    /// </summary>
    public DateTime BirthDay { get; set; }
    /// <summary>
    /// Указание - является ли пользователь админом
    /// </summary>
    public bool Admin { get; set; }

    public static UserEntity ToUser(UserDetails userDetails, string createdBy)
    {
        var user = new UserEntity()
        {
            Login = userDetails.Login,
            Password = userDetails.Password,
            Name = userDetails.Name,
            Gender = userDetails.Gender,
            BirthDay = userDetails.BirthDay,
            Admin = userDetails.Admin,
            CreatedOn = DateTime.Now,
            CreatedBy = createdBy,
            RevokedBy = null,
            RevokedOn = null,
            ModifiedBy = createdBy,
            ModifiedOn = DateTime.Now,
        };

        return user;
    }
}
