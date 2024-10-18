using AccountingTM.Dto.Common;

namespace AccountingTM.Dto.Application
{
	public class GetAllApplicationDto : SearchPagedRequestDto
	{
		public int? TechnicalEquipmentId { get; set; }
	}
}
