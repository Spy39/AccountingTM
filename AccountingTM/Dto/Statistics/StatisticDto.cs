namespace AccountingTM.Dto.Statistics
{
    public class StatisticDto
    {
        public TechicalEquipmentStatisticDto TechicalEquipment { get; set; }
        public ApplicationStatisticDto Application { get; set; }
        public ConsumableStatisticDto Consumable { get; set; }

        public List<FaultsByMonthDto> FaultsByMonth { get; set; } = new();
        public List<FaultyEquipmentDto> FaultyEquipment { get; set; } = new();
        public List<TopConsumablesDto> TopConsumables { get; set; } = new();
        public List<AvgClosureTimeDto> AvgClosureTime { get; set; } = new();
        public List<FaultCategoryDto> FaultCategories { get; set; } = new();
    }

    // Кол-во неисправностей за 12 месяцев
    public class FaultsByMonthDto
    {
        public string Month { get; set; } // Название месяца
        public int FaultCount { get; set; } // Количество неисправностей
    }

    // Топ-5 неисправных ТС
    public class FaultyEquipmentDto
    {
        public string EquipmentModel { get; set; } // Модель ТС
        public string Brand { get; set; } // Бренд ТС
        public int FaultCount { get; set; } // Количество поломок
    }

    // Топ-5 расходных материалов
    public class TopConsumablesDto
    {
        public string ConsumableName { get; set; } // Название расходника
        public int UsageCount { get; set; } // Количество использований
    }

    // Среднее время закрытия заявки
    public class AvgClosureTimeDto
    {
        public string Category { get; set; } // Категория заявок
        public double AvgDays { get; set; } // Среднее время закрытия в днях
    }

    // Частые категории неисправностей
    public class FaultCategoryDto
    {
        public string CategoryName { get; set; } // Название категории
        public int Count { get; set; } // Количество случаев
    }
}
