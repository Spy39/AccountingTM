using Accounting.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
		public DateTime Date { get; set; }
		/// <summary>
		/// Состояние изделия
		/// </summary>
		public string ProductCondition { get; set; }
		/// <summary>
		/// Основание
		/// </summary>
		public string Base { get; set; }
		/// <summary>
		/// Предприятие и ФИО сдавшего
		/// </summary>
		public string Passed { get; set; }
		/// <summary>
		/// Предприятие и ФИО принявшего
		/// </summary>
		public string Accepted { get; set; }
		/// <summary>
		/// Примечание
		/// </summary>
		public string? Note { get; set; }
	}
}
