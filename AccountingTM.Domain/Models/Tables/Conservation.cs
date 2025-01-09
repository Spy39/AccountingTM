using AccountingTM.Domain.Models.Directory;
using AccountingTM.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace AccountingTM.Domain.Models.Tables
{
    /// <summary>
    /// Консервация
    /// </summary>
    public class Conservation : Entity
    {
        public int TechnicalEquipmentId { get; set; }
        [ForeignKey(nameof(TechnicalEquipmentId))]
        public TechnicalEquipment? TechnicalEquipment { get; set; }
        public int EmployeeId { get; set; }
        [ForeignKey(nameof(EmployeeId))]
        public Employee Employee { get; set; }
        /// <summary>Дата</summary>
        public DateTime Date { get; set; }
        /// <summary>Наименование работ</summary>
        public string NameOfWorks { get; set; }
        /// <summary>Срок действия работ</summary>
        public DateTime Validity { get; set; }
    }
}
