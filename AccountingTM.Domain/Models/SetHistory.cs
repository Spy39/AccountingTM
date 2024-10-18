using Accounting.Models;
using AccountingTM.Domain.Models.Directory;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingTM.Domain.Models
{
    /// <summary>
    /// Обозначение комплекта
    /// </summary>
    public class SetHistory : Entity
    {
		public int EmployeeId { get; set; }
		[ForeignKey(nameof(EmployeeId))]
		public Employee? Employee { get; set; } //Ответственный			
		/// <summary>Дата операции</summary>///
		public DateTime? DateOfOperation { get; set; }
		/// <summary>Тип операции</summary>///
		public string? TypeOfOperation { get; set; }
	}
}