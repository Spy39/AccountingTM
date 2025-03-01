using Accounting.Data;
using AccountingTM.Domain.Authorization;
using AccountingTM.Domain.Permissions;
using AccountingTM.Dto.Permissions;
using AccountingTM.Localization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AccountingTM.Controllers
{
    [Authorize]
    public class PermissionController : Controller
    {
        private readonly DataContext _context;
        private readonly LocalizationManager _localizationManager;

        public PermissionController(DataContext context, LocalizationManager localizationManager)
        {
            _context = context;
            _localizationManager = localizationManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetAll()
        {
            var permissions = PermissionProvider.Permissions;
            var result = new List<PermissionTreeItemDto>(permissions.Count);
            foreach (var permission in permissions)
            {
                MapPermissionToTreeItem(result, permission);
            }

            return Ok(result);
        }

        public IActionResult GetAllByRole (int id)
        {
            var grantedPermissionNames = _context.RolePermissions.Where(x => x.RoleId == id && x.IsGranted).Select(x => x.Name).ToList();
            var permissions = PermissionProvider.Permissions;
            var result = new List<PermissionTreeItemDto>(permissions.Count);
            foreach (var permission in permissions)
            {
                MapPermissionToTreeItem(result, permission, grantedPermissionNames);
            }

            return Ok(result);
        }

         [HttpPost("[controller]/[action]")]
        public IActionResult UpdatePermissions([FromBody] UpdatePermissionsDto input)
        {
            var role = _context.Roles.Find(input.RoleId);
            if (role == null)
            {
                return NotFound(new { message = $"Роль с ID {input.RoleId} не найдена" });
            }

            // Удаляем старые разрешения
            var existingPermissions = _context.RolePermissions.Where(x => x.RoleId == input.RoleId).ToList();
            _context.RolePermissions.RemoveRange(existingPermissions);

            // Добавляем новые
            foreach (var permissionName in input.PermissionNames)
            {
                _context.RolePermissions.Add(new RolePermission
                {
                    RoleId = input.RoleId,
                    Name = permissionName,
                    IsGranted = true
                });
            }

            _context.SaveChanges();
            return Ok();
        }

        private void MapPermissionToTreeItem(IList<PermissionTreeItemDto> result, Permission permission, IList<string>? grantedPermissionNames = null)
        {
            var permissionTreeItem = new PermissionTreeItemDto
            {
                Id = Guid.NewGuid(),
                Text = _localizationManager.GetString(permission.Name),
                Icon = permission.Icon,
                Children = new List<PermissionTreeItemDto>(),
                Checked = grantedPermissionNames != null && grantedPermissionNames.Contains(permission.Name)
            };

            if (permission.Children != null)
            {
                foreach (var child in permission.Children)
                {
                    MapPermissionToTreeItem(permissionTreeItem.Children, child, grantedPermissionNames);
                }
            }

            result.Add(permissionTreeItem);
        }


    }
}
