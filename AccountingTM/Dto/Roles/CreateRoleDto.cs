namespace AccountingTM.Dto.Roles
{
    public class CreateRoleDto
    {
        public string Name { get; set; }
        public IEnumerable<string> PermissionNames { get; set; }
    }
}
