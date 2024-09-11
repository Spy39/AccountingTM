using AccountingTM.Domain.Models.Directory;
using System.ComponentModel.DataAnnotations.Schema;

namespace AccountingTM.ViewModels.Consumable
{
	public class ConsumableViewModel
	{
		public int ConsumableId { get; set; }
		public string TypeConsumable { get; set; }
		public string Brand { get; set; }
		public string Model { get; set; }
		public string Location { get; set; }
		public string Unit { get; set; }
		public string Quantity { get; set; }
	}
}
