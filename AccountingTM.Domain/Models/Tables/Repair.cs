using AccountingTM.Models;
using System.ComponentModel.DataAnnotations.Schema;
namespace AccountingTM.Domain.Models.Tables
{
    /// <summary>
    /// Ремонт
    /// </summary>
    public class Repair : Entity
    {
        public int TechnicalEquipmentId { get; set; }
        [ForeignKey(nameof(TechnicalEquipmentId))]
        public TechnicalEquipment? TechnicalEquipment { get; set; }
        public DateTime Date { get; set; }
        /// <summary>Предприятие</summary>
        public string Company { get; set; }
        /// <summary>Причина поступления в ремонт</summary>
        public string ReasonForRepair { get; set; }
        /// <summary>Сведения о производственном ремонте</summary>
        public string RepairInformation { get; set; }
    }
}
