using AccountingTM.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace AccountingTM.Domain.Models.Tables
{
    /// <summary>
    /// Учет выполненных работ
    /// </summary>
    public class CompletedWork : Entity
    {
        public int TechnicalEquipmentId { get; set; }
        [ForeignKey(nameof(TechnicalEquipmentId))]
        public TechnicalEquipment? TechnicalEquipment { get; set; }
        public int ApplicationsId { get; set; }
        [ForeignKey(nameof(ApplicationsId))]
        public Application Applications { get; set; }
        /// <summary>Дата</summary>
        public DateTime Date { get; set; }
        /// <summary>Наименование работ и их причина</summary>
        public string NameAndReason { get; set; }
        /// <summary>Фамилия выполнившего работу</summary>
        public string Completed { get; set; }
    }
}
