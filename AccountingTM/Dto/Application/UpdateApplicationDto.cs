using AccountingTM.Domain.Enums;

namespace AccountingTM.Dto.Application
{
    public class UpdateApplicationDto
    {
        public int ApplicationId { get; set; }
        public ApplicationStatus Status { get; set; }
        public Priority Priority { get; set; }
        public int CategoryId { get; set; }
    }
}
