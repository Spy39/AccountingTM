using AccountingTM.Models;

namespace AccountingTM.Domain.Models.Tables
{
    public class EquipmentConsumableUsage : Entity
    {
        public int TechnicalEquipmentId { get; set; }
        public TechnicalEquipment TechnicalEquipment { get; set; }

        public int ConsumableId { get; set; }
        public Consumable Consumable { get; set; }

        public double QuantityUsed { get; set; }

        public DateTime UsageDate { get; set; } = DateTime.Now;
    }
}
