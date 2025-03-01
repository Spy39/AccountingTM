using AccountingTM.Dto.Common;
namespace AccountingTM.Dto.Consumable
{
    public class GetAllConsumableHistoryDto : SearchPagedRequestDto
    {
        public int ConsumableId { get; set; }
        public bool IsWithoutSet { get; set; }
        public string? SearchQuery { get; set; }
        public bool? IsSupply { get; set; }
        public int? TechnicalEquipmentId { get; set; }
        public string? StartDate { get; set; }
        public string? EndDate { get; set; }
    }
}
