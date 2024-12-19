using AccountingTM.Domain.Models.Directory;
using AccountingTM.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingTM.Domain.Models.Tables
{
    public class TechnicalEquipmentHistory : Entity
    {
        public int TechnicalEquipmentId { get; set; }
        [ForeignKey(nameof(TechnicalEquipmentId))]
        public TechnicalEquipment? TechnicalEquipment { get; set; }
        public int? EmployeeId { get; set; }
        public Employee Employee { get; set; } // Исполнитель
        /// <summary>
        /// Дата
        /// </summary>
        public DateTime? Date { get; set; } = DateTime.UtcNow;
        public string TypeOfOperation { get; set; }
        public string? Name { get; set; }
    }
}
