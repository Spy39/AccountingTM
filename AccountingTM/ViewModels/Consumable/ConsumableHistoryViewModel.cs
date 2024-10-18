using AccountingTM.Domain.Models.Directory;
using System.ComponentModel.DataAnnotations.Schema;

namespace AccountingTM.ViewModels.Consumable
{
	public class ConsumableHistoryViewModel
	{
		public int ConsumableId { get; set; }
		public int EmployeeId { get; set; }
		public string Employee { get; set; }
		public double Quantity { get; set; }
		public DateTime? DateLatestAddition { get; set; }
		/// <summary>Дата операции</summary>///
		public bool IsSupply { get; set; }
		/// <summary>Тип операции</summary>///
		public string? TypeOfOperation => IsSupply ? "Списание" : "Пополнение";
		/// <summary>Комментарий</summary>///
		public string? Comment { get; set; }

	}
}
