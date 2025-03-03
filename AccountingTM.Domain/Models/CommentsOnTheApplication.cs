using AccountingTM.Domain.Models.Directory;
using System.ComponentModel.DataAnnotations.Schema;

namespace AccountingTM.Domain.Models
{
    public class CommentsOnTheApplication : Entity
    {
        public int ApplicationId { get; set; }
        [ForeignKey(nameof(ApplicationId))]
        public Application? Application { get; set; }
        public int EmployeeId { get; set; } // 🔹 ID пользователя, добавившего комментарий
        public Employee Employee { get; set; } // 🔹 Связь с таблицей пользователей
        /// <summary>Дата</summary>
        public DateTime Date { get; set; } = DateTime.Now;
        /// <summary>Текст комментария</summary>
        public string Text { get; set; }
        /// <summary>Путь до файла</summary>
        public string? PathToFile { get; set; }
    }
}
