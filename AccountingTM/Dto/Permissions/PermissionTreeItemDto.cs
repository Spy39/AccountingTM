using AccountingTM.Dto.Common;

namespace AccountingTM.Dto.Permissions
{
    public class PermissionTreeItemDto
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
        public IList<PermissionTreeItemDto>? Children { get; set; }
        public string Icon { get; set; }
        public bool Checked { get; set; }
    }
}
