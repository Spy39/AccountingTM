using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingTM.Domain.Enums
{
    /// <summary>
    /// Состояние технического средства
    /// </summary>
    public enum ConditionEquipment
    {
        /// <summary>Исправно</summary>
        [Description("Исправно")]
        Serviceable,
		/// <summary>Неисправно</summary>
		[Description("Неисправно")]
		Faulty,
		/// <summary>Работоспособно</summary>
		[Description("Работоспособно")]
		Efficient,
		/// <summary>Неработоспособно</summary>
		[Description("Неработоспособно")]
		Inoperative
    }
}
