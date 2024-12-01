using AccountingTM.Dto.Common;

namespace AccountingTM.Dto.Set
{
    public class GetAllCompoundSetDto : PagedRequestDto
    {
        public int SetId { get; set; }
    }
}
