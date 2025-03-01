using AccountingTM.Domain.Models.Directory;
using AccountingTM.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace AccountingTM.Domain.Models
{
    /// <summary>
    /// История операций расходного материала
    /// </summary>
    public class ConsumableHistory : Entity
    {
        public int? TechnicalEquipmentId { get; set; }
        [ForeignKey(nameof(TechnicalEquipmentId))]
        public TechnicalEquipment? TechnicalEquipment { get; set; }
        public Consumable Consumable { get; set; }
        public int ConsumableId { get; set; }
        public int EmployeeId { get; set; }
        [ForeignKey(nameof(EmployeeId))]
        public Employee? Employee { get; set; }	
        /// <summary>Количество</summary>
        public double Quantity { get; set; }
        /// <summary>Количество после операции</summary>
        public double QuantityAfterOperation { get; set; }
        /// <summary>Дата операции</summary>///
        public DateTime? DateOfOperation { get; set; }
        /// <summary>Тип операции</summary>///
        public bool IsSupply { get; set; }
        /// <summary>Тип операции</summary>///
        public string? TypeOfOperation => IsSupply ? "Пополнение" : "Списание";
        /// <summary>Комментарий</summary>///
        public string? Comment { get; set; }


    }
}
