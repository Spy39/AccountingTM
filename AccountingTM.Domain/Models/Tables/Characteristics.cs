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
        public int IndicatorsId { get; set; }
        [ForeignKey(nameof(IndicatorsId))]
        public Indicators Indicator { get; set; }
        public int UnitsId { get; set; }
        [ForeignKey(nameof(UnitsId))]
        public Unit Unit { get; set; }
        public string Meaning { get; set; }
    }
}
