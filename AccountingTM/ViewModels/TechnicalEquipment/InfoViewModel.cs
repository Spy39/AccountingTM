using AccountingTM.Domain.Models.Directory;

namespace AccountingTM.ViewModels.TechnicalEquipment
{
    public class InfoViewModel
    {
        public int TechnicalId { get; set; }
        public AccountingTM.Domain.Models.Set? Set { get; set; }
        public string TypeEquipment { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string? SerialNumber { get; set; }
        public string InventoryNumber { get; set; }
        public Employee Employee { get; set; }
        public Location Location { get; set; }
        /// <summary>
        /// Дата изготовления
        /// </summary>
        public DateTime? Date { get; set; }
        /// <summary>
        /// Дата ввода в эксплуатацию
        /// </summary>
        public DateTime? DateStart { get; set; }
        /// <summary>
        /// Средний срок работы
        /// </summary>
        public int? WorkTimeAvg { get; set; }
        /// <summary>
        /// Дата действия гарантии
        /// </summary>
        public DateTime? DateGarant { get; set; }
        public string Status { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedDate { get; set; }
    }
}
