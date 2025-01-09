using AccountingTM.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace AccountingTM.Domain.Models.Tables
{
    /// <summary>
    /// Прием и передача изделия
    /// </summary>
    public class ReceptionAndTransmission : Entity
    {
        public int TechnicalEquipmentId { get; set; }
        [ForeignKey(nameof(TechnicalEquipmentId))]
        public TechnicalEquipment? TechnicalEquipment { get; set; }
        /// <summary>Дата</summary>
        public DateTime Date { get; set; }
        /// <summary>Состояние изделия</summary>
        public string ProductCondition { get; set; }
        /// <summary>Основание</summary>
        public string Base { get; set; }
        /// <summary>Предприятие и ФИО сдавшего</summary>
        public string Passed { get; set; }
        /// <summary>Предприятие и ФИО принявшего</summary>
        public string Accepted { get; set; }
        /// <summary>Примечание</summary>
        public string? Note { get; set; }
    }
}
