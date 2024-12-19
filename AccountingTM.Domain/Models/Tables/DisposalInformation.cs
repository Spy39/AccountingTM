using System;
using System.Collections.Generic;
using Accounting.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccountingTM.Models;

namespace AccountingTM.Domain.Models.Tables
{
    /// <summary>
    /// Сведения об утилизации
    /// </summary>
    public class DisposalInformation : Entity
	{
		public int TechnicalEquipmentId { get; set; }
		[ForeignKey(nameof(TechnicalEquipmentId))]
		public TechnicalEquipment? TechnicalEquipment { get; set; }
		public DateTime Date {  get; set; }
		/// <summary>
		/// Описание
		/// </summary>
		public string Description { get; set; }
		/// <summary>
		/// Примечание
		/// </summary>
		public string? Note { get; set; }
	}
}
