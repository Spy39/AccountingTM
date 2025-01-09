using AccountingTM.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace AccountingTM.Domain.Models.Tables
{
    /// <summary>
    /// Хранение
    /// </summary>
    public class Storage : Entity
    {
        public int TechnicalEquipmentId { get; set; }
        [ForeignKey(nameof(TechnicalEquipmentId))]
        public TechnicalEquipment? TechnicalEquipment { get; set; }
        /// <summary>Дата приемки на хранение</summary>
        public DateTime Acceptance { get; set; }
        /// <summary>Дата снятия с хранения</summary>
        public DateTime Removal { get; set; }
        /// <summary>Условия хранения</summary>
        public string StorageConditions { get; set; }
        /// <summary>Вид хранения</summary>
        public string TypeOfStorage { get; set; }
    }
}
