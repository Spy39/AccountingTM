using AccountingTM.Domain.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace Accounting.Models
{
    /// <summary>
    /// Неисправность
    /// </summary>
    public class Malfunction : Entity
    {
        public string? Date { get; set; }
        public string? Name { get; set; }
        public string? Manifestation { get; set; }
        public string? Critical { get; set; }
        public string? DateOfSolve { get; set; }

        public int TechnicalEquipmentId { get; set; }

        [ForeignKey("TechnicalEquipmentId")]
        public TechnicalEquipment TechnicalEquipment { get; set; }
    }
}
