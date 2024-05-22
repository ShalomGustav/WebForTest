using System.ComponentModel.DataAnnotations;

namespace WebForTest.Models;

public class UserDetails
{
    /// <summary>
    /// Уникальный Логин (запрещены все символы кроме латинских букв и цифр),
    /// </summary>
    [RegularExpression(@"^[a-zA-Z0-9]+$")]
    public string Login { get; set; }
    /// <summary>
    /// Пароль(запрещены все символы кроме латинских букв и цифр)
    /// </summary>
    [RegularExpression(@"^[a-zA-Z0-9]+$")]
    public string Password { get; set; }
    /// <summary>
    /// Имя (запрещены все символы кроме латинских и русских букв)
    /// </summary>
    [RegularExpression(@"^[a-zA-Zа-яА-Я]+$")]
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
}
