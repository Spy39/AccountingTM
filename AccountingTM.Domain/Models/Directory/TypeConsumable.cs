using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingTM.Domain.Models.Directory
{
	/// <summary>
	/// Тип расходного материала
	/// </summary>
	public class TypeConsumable : Entity
	{
		public string Name { get; set; }
	}
}
