using AccountingTM.Domain.Models.Directory;
using AccountingTM.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace AccountingTM.Domain.Models.Tables
{
    /// <summary>
    /// Характеристики технического средства
    /// </summary>
    public class Characteristic : Entity
    {
        public int TechnicalEquipmentId { get; set; }
        [ForeignKey(nameof(TechnicalEquipmentId))]
        public TechnicalEquipment? TechnicalEquipment { get; set; }
        public int IndicatorId { get; set; }
        [ForeignKey(nameof(IndicatorId))]
        public Indicator Indicator { get; set; }
        public int UnitId { get; set; }
        [ForeignKey(nameof(UnitId))]
        public Unit Unit { get; set; }
        /// <summary>Значение</summary>
        public string Meaning { get; set; }
        public int? ModelId { get; set; }
        public Model Model { get; set; }
    }
}