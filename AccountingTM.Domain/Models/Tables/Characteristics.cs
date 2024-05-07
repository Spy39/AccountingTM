using AccountingTM.Domain.Models.Directory;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingTM.Domain.Models.Tables
{
    /// <summary>
    /// Характеристики технического средства
    /// </summary>
    public class Characteristics : Entity
    {
        public int IndicatorId { get; set; }
        [ForeignKey(nameof(IndicatorId))]
        public Indicator Indicator { get; set; }
        public int UnitId { get; set; }
        [ForeignKey(nameof(UnitId))]
        public Unit Unit { get; set; }
        public string Meaning { get; set; }
    }
}
