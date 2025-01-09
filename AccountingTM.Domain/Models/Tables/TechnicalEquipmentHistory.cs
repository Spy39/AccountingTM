using AccountingTM.Domain.Models.Directory;
using AccountingTM.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace AccountingTM.Domain.Models.Tables
{
    public class TechnicalEquipmentHistory : Entity
    {
        public int TechnicalEquipmentId { get; set; }
        [ForeignKey(nameof(TechnicalEquipmentId))]
        public TechnicalEquipment? TechnicalEquipment { get; set; }
        public int? EmployeeId { get; set; }
        public Employee Employee { get; set; } // Исполнитель
        /// <summary>Дата</summary>
        public DateTime? Date { get; set; } = DateTime.UtcNow;
        /// <summary>Тип операции</summary>
        public string TypeOfOperation { get; set; }
        /// <summary>Наименование операции</summary>
        public string? Name { get; set; }
    }
}
