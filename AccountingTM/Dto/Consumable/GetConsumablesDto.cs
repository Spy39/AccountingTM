using AccountingTM.Dto.Common;

namespace AccountingTM.Dto.Consumable
{
    public class GetConsumablesDto : PagedRequestDto
    {
        public string? SearchQuery { get; set; } // Поиск по модели, бренду и т. д.
        public bool? IsAvailable { get; set; } // true - В наличии, false - Нет в наличии, null - Все
    }
}