using AccountingTM.Domain.Models.Directory;
using System.ComponentModel.DataAnnotations.Schema;

namespace AccountingTM.ViewModels.Consumable
{
	public class ConsumableViewModel
	{
		public int ConsumableId { get; set; }
		public int TypeConsumableId { get; set; }
		public int BrandId { get; set; }
		public int LocationId { get; set; }
		public int UnitId { get; set; }
		public string TypeConsumable { get; set; }
		public string Brand { get; set; }
		public string Model { get; set; }
		public string Location { get; set; }
		public string Unit { get; set; }
		public double Quantity { get; set; }
		public string? Status { get; set; }
		public DateTime? DateLatestAddition { get; set; }
	}
}
