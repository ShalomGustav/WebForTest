namespace WebForTest.Models
{
    public class User : Entity
    {
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
    }
}
