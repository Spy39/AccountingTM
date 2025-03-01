using AccountingTM.Dto.Common;

namespace AccountingTM.Dto.Roles
{
    public class UpdateRoleDto : EntityDto
    {
        public string Name { get; set; }
        public IEnumerable<string> PermissionNames { get; set; }
    }
}
