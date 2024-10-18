using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingTM.Forecasting
{
	public class ConsumableAnalisisModel
	{
		public string TypeConsumable { get; set; } //Тип расходного материала
		public string Brand { get; set; } //Бренд		
		/// <summary>
		/// Модель расходного материала
		/// </summary>
		public string Model { get; set; }
		/// <summary>Количество</summary>
		public float Quantity { get; set; }
		public int Mounth { get; set; }
		public int Year { get; set; }
	}
}
