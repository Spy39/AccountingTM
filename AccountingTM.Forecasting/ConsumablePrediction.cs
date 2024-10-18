using Microsoft.ML.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingTM.Forecasting
{
	public class ConsumablePrediction
	{
		[ColumnName("Score")]
		public float Quantity { get; set; }

	}
}
