namespace WebForTest.Models
{
    public class Entity
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
    }
}
