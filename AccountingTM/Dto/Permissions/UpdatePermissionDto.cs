namespace AccountingTM.Dto.Permissions
{
    public class UpdatePermissionsDto
    {
        public int RoleId { get; set; }  // ID роли, которой назначаем права
        public List<string> PermissionNames { get; set; } = new(); // Список прав
    }
}
