using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingTM.Domain.Models.Directory
{
	/// <summary>
	/// Категория заявки
	/// </summary>
	public class Category : Entity
	{
		public string Name { get; set; }
	}
}
