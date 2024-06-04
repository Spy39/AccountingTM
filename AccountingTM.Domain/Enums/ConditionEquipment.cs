using System;
using System.Collections.Generic;
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
        Serviceable,
    /// <summary>Неисправно</summary>
        Faulty,
    /// <summary>Работоспособно</summary>
        Efficient,
    /// <summary>Неработоспособно</summary>
        Inoperative
    }
}
