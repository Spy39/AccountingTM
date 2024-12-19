using AccountingTM.Domain.Models.Directory;
using Accounting.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccountingTM.Models;

namespace AccountingTM.Domain.Models.Tables
{
    /// <summary>
    /// Консервация
    /// </summary>
    public class Conservation : Entity
	{
		public int TechnicalEquipmentId { get; set; }
		[ForeignKey(nameof(TechnicalEquipmentId))]
		public TechnicalEquipment? TechnicalEquipment { get; set; }
		public int EmployeeId { get; set; }
		[ForeignKey(nameof(EmployeeId))]
		public Employee Employee { get; set; }
		public DateTime Date { get; set; }
		/// <summary>
		/// Наименование работ
		/// </summary>
		public string NameOfWorks { get; set; }
		/// <summary>
		/// Срок действия работ
		/// </summary>
		public DateTime Validity {  get; set; }
	}
}
