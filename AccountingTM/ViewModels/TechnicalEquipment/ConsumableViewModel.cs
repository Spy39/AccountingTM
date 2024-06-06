using AccountingTM.Domain.Models.Directory;

namespace AccountingTM.ViewModels.TechnicalEquipment
{
    public class ConsumableViewModel
    {
		public int TechnicalId { get; set; }
		public string TypeConsumable { get; set; }
		public string Brand { get; set; }
		public string Model { get; set; }
		public string? SerialNumber { get; set; }
		public string? Employee { get; set; }
		public string Location { get; set; }
		public string Unit { get; set; }
		public string Quantity { get; set; }

	}
}
