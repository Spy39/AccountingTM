using Accounting.Data;
using AccountingTM.Domain.Authorization;
using AccountingTM.Domain.Models;
using AccountingTM.Domain.Permissions;
using AccountingTM.Dto.Common;
using AccountingTM.Dto.Roles;
using AccountingTM.Exceptions;
using AccountingTM.Localization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AccountingTM.Controllers
{
    [Authorize]
    public class RolesController : Controller
    {
        private readonly DataContext _context;
        private readonly LocalizationManager _localizationManager;

        public RolesController(DataContext context, LocalizationManager localizationManager)
        {
            _context = context;
            _localizationManager = localizationManager;
        }

        [HttpGet("[controller]/[action]")]
        public IActionResult GetAll([FromQuery] SearchPagedRequestDto input)
        {
            IQueryable<Role> query = _context.Roles;
            if (!string.IsNullOrWhiteSpace(input.SearchQuery))
            {
                var keyword = input.SearchQuery.ToLower();
                query = query.Where(x => x.Name.ToLower().Contains(keyword));
            }

            var entities = query.Skip(input.SkipCount).Take(input.MaxResultCount).ToList();
            return Ok(new PagedResultDto<Role>(query.Count(), entities));
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Get(int id)
        {
            var entity = _context.Roles.Find(id);
            if (entity == null)
            {
                throw new Exception($"Роль с id = {id} не найдена");
            }

            return Ok(entity);
        }

        [HttpPost]
        public IActionResult Create([FromBody] CreateRoleDto input)
        {
            if (string.IsNullOrWhiteSpace(input.Name))
            {
                throw new UserFriendlyException("Название роли не может быть пустым!");
            }

            if (_context.Roles.Any(x => x.Name == input.Name))
            {
                throw new UserFriendlyException("Роль с таким названием уже существует!");
            }

            var newRole = new Role
            {
                Name = input.Name
            };

            _context.Roles.Add(newRole);
            _context.SaveChanges();

            if (input.PermissionNames != null && input.PermissionNames.Any())
            {
                var permissions = PermissionProvider.Permissions;

                Permission? currentPermission = null;

                foreach (var permissionName in input.PermissionNames)
                {
                    var permission = permissions.FirstOrDefault(x => _localizationManager.GetString(x.Name) == permissionName);
                    if (permission != null)
                    {
                        currentPermission = permission;
                        _context.RolePermissions.Add(new RolePermission
                        {
                            RoleId = newRole.Id,
                            Name = permission.Name,
                            IsGranted = true
                        });
                    }
                    else if (currentPermission != null)
                    {
                        var childPermission = currentPermission.Children.FirstOrDefault(x => _localizationManager.GetString(x.Name) == permissionName);
                        if (childPermission != null)
                        {
                            _context.RolePermissions.Add(new RolePermission
                            {
                                RoleId = newRole.Id,
                                Name = childPermission.Name,
                                IsGranted = true
                            });
                        }
                    }

                }

            }

            _context.SaveChanges();
            return Ok();
        }

        [HttpPost]
        public IActionResult Update([FromBody] UpdateRoleDto input)
        {
            var role = _context.Roles.AsNoTracking().FirstOrDefault(x => x.Id == input.Id);
            if (role == null)
            {
                throw new Exception($"Роль с id = {input.Id} не найдена");
            }
            

            if (!string.IsNullOrWhiteSpace(input.Name))
            {
                if (_context.Roles.Any(x => x.Name == input.Name && x.Id != role.Id))
                {
                    throw new UserFriendlyException("Роль с таким названием уже существует!");
                }
            }

                role.Name = input.Name;
            _context.Roles.Update(role);
            _context.SaveChanges();

            var currentPermissions = _context.RolePermissions.Where(x => x.RoleId == role.Id).ToList();
            _context.RolePermissions.RemoveRange(currentPermissions);
            _context.SaveChanges();

            var permissions = PermissionProvider.Permissions;
            Permission? currentPermission = null;
            
            foreach(var permissionName in input.PermissionNames)
            {
                var permission = permissions.FirstOrDefault(x => _localizationManager.GetString(x.Name) == permissionName);
                if(permission != null)
                {
                    currentPermission = permission;
                    _context.RolePermissions.Add(new RolePermission
                    {
                        RoleId = role.Id,
                        Name = permission.Name,
                        IsGranted = true
                    });
                }
                else if (currentPermission != null)
                {
                    var childPermission = currentPermission.Children.FirstOrDefault(x => _localizationManager.GetString(x.Name) == permissionName);
                    if(childPermission != null)
                    {
                        _context.RolePermissions.Add(new RolePermission
                        {
                            RoleId = role.Id,
                            Name = childPermission.Name,
                            IsGranted = true
                        });
                    }
                }

            }
            _context.SaveChanges();
            return Ok();
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            Role list = _context.Roles.Find(id);
            if (list == null)
            {
                return NotFound();
            }
            _context.Roles.Remove(list);
            _context.SaveChanges();
            return Ok();
        }

    }
}
