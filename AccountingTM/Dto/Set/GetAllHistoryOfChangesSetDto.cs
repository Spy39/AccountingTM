using AccountingTM.Dto.Common;

namespace AccountingTM.Dto.Set
{
	public class GetAllHistoryOfChangesSetDto : SearchPagedRequestDto
	{
		public int SetId { get; set; }
	}
}
