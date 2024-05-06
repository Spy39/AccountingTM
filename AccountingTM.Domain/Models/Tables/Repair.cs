using Accounting.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace AccountingTM.Domain.Models.Tables
{
	/// <summary>
	/// Ремонт
	/// </summary>
	public class Repair : Entity
	{
		public int TechnicalEquipmentId { get; set; }
		[ForeignKey(nameof(TechnicalEquipmentId))]
		public TechnicalEquipment? TechnicalEquipment { get; set; }
		public DateTime Date { get; set; }
		/// <summary>
		/// Предприятие
		/// </summary>
		public string Company { get; set; }
		/// <summary>
		/// Наработка с начала эксплуатации
		/// </summary>
		public string InitialOperatingTime { get; set; }
		/// <summary>
		/// Наработка после последнего ремонта
		/// </summary>
		public string HoursAfterRepair { get; set; }
		/// <summary>
		/// Причина поступления в ремонт
		/// </summary>
		public string ReasonForRepair { get; set; }
		/// <summary>
		/// Сведения о производственном ремонте
		/// </summary>
		public string RepairInformation { get; set; }
	}
}
