using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingTM.Domain.Enums
{
	/// <summary>
	/// Приоритет заявки
	/// </summary>
	public enum Priority
	{
		/// <summary>
		/// Критический
		/// </summary>
		Critical,
		/// <summary>
		/// Высокий
		/// </summary>
		High,
		/// <summary>
		/// Нормальный
		/// </summary>
		Normal,
		/// <summary>
		/// Низкий
		/// </summary>
		Low
	}
}
