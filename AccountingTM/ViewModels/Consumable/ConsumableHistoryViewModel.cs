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
        public DateTime? DateOfOperation { get; set; }
        // Если IsSupply == true, значит операция – Пополнение, иначе – Списание
        public bool IsSupply { get; set; }
        /// <summary>Тип операции</summary>///
        public string? TypeOfOperation => IsSupply ? "Списание" : "Пополнение";
        public string TechnicalEquipment { get; set; }
        /// <summary>Комментарий</summary>///
        public string? Comment { get; set; }

    }
}
