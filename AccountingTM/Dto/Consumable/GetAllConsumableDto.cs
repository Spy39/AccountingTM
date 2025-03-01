using AccountingTM.Dto.Common;

namespace AccountingTM.Dto.Consumable
{
    public class GetAllConsumableDto : SearchPagedRequestDto
    {
        public int ConsumableId { get; set; }
        public bool IsWithoutSet { get; set; }
    }
}
