namespace AccountingTM.Dto.Statistics
{
    public class ConsumableStatisticDto
    {
        public int TotalCount { get; set; } //всего
        public int InStockCount { get; set; } //В наличии
        public int LowStockCount { get; set; } //малый запас
        public int OutOfStockCount { get; set; } //нет в наличии
        public double AvgUsagePerMonth { get; set; } // Средний расход материалов
        public string MostUsedConsumable { get; set; } // Самый часто используемый материал
    }
}
