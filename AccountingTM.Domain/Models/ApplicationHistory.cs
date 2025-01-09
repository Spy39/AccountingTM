using AccountingTM.Domain.Models.Directory;
using System.ComponentModel.DataAnnotations.Schema;

namespace AccountingTM.Domain.Models
{
    public class ApplicationHistory : Entity
    {
        public int ApplicationId { get; set; }
        [ForeignKey(nameof(ApplicationId))]
        public Application? Application { get; set; }
        public int? EmployeeId { get; set; }
        public Employee Employee { get; set; } // Исполнитель
        /// <summary>Дата</summary>
        public DateTime Date { get; set; } = DateTime.Now;
        /// <summary>Тип операции</summary>
        public string TypeOfOperation { get; set; }
        /// <summary>Наименование операции</summary>
        public string Name { get; set; }
    }
}
