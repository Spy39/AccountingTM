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
    public class Set : Entity
    {

		public int? LocationId { get; set; }
		public Location? Location { get; set; } //Местоположение
		public int? EmployeeId { get; set; }
		public Employee? Employee { get; set; } //Ответственный			
		/// <summary>Наименование комплекта</summary>///
		public string Name { get; set; }
		/// <summary>Статус комплекта</summary>///
		public bool IsStatusSet { get; set; }
		public string StatusSet => IsStatusSet ? "Полный" : "Неполный";
	}
}