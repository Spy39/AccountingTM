using AccountingTM.Domain.Enums;
using AccountingTM.Domain.Models.Directory;
using AccountingTM.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace AccountingTM.Domain.Models
{
    /// <summary>
    /// Учет заявок
    /// </summary>
    public class Application : Entity
    {
        public int LocationId { get; set; }
        [ForeignKey(nameof(LocationId))]
        public Location Location { get; set; } //Местоположение
        public int CategoryId { get; set; }
        [ForeignKey(nameof(CategoryId))]
        public Category Category { get; set; } // Категория
        public int? EmployeeId { get; set; }
        public Employee Employee { get; set; } // Исполнитель
        public int? TechnicalEquipmentId { get; set; }
        public TechnicalEquipment TechnicalEquipment { get; set; }
        /// <summary>Номер заявки</summary>
        public string ApplicationNumber { get; set; }
        /// <summary>Дата создания</summary>
        public DateTime DateOfCreation { get; set; }
        /// <summary>Дата изменения</summary>
        public DateTime DateOfChange { get; set; }
        /// <summary>Срок истечения</summary>
        public DateTime? ExpirationDate { get; set; }
        /// <summary>Тема</summary>
        public string Subject { get; set; }
        ///<summary>Описание</summary>
        public string Description { get; set; }
        /// <summary>Статус</summary>
        public ApplicationStatus Status { get; set; }
        /// <summary>Автор</summary>
        public string Author { get; set; }
        /// <summary>Последний ответивший</summary>
        public string? LastReply { get; set; }
        public Priority Priority { get; set; }


        public string GetApplicationStatus()
        {
            switch (Status)
            {
                case ApplicationStatus.New:
                    return "Новая";
                case ApplicationStatus.CommentReceived:
                    return "Получен комментарий";
                case ApplicationStatus.CommentSent:
                    return "Комментарий отправлен";
                case ApplicationStatus.InProgress:
                    return "В работе";
                case ApplicationStatus.Suspended:
                    return "Приостановлена";
                case ApplicationStatus.Transferred:
                    return "Передана";
                case ApplicationStatus.Solved:
                    return "Решена";
                default: return "";

            }
        }

        public string GetApplicationPrioity()
        {
            switch (Priority)
            {
                case Priority.Critical:
                    return "Критический";
                case Priority.High:
                    return "Высокий";
                case Priority.Normal:
                    return "Нормальный";
                case Priority.Low:
                    return "Низкий";
                default: return "";

            }
        }
    }
}
