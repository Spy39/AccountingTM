using AccountingTM.Dto.Common;

namespace AccountingTM.Dto.TechnicalEquipment
{
    public class GetAllTechnicalDto : SearchPagedRequestDto
    {
         public int ConsumableId { get; set; }
        public bool IsWithoutSet { get; set; }
    }
}
