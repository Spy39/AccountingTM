using Accounting.Data;
using AccountingTM.Domain.Authorization;
using AccountingTM.Domain.Models;
using AccountingTM.Domain.Permissions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingTM.Domain.Seeds
{
    public class RoleSeed
    {
        private readonly DataContext _context;

        public RoleSeed(DataContext context)
        {
            _context = context;
        }

        public void Seed()
        {
            var admin = _context.Roles.FirstOrDefault(x => x.Name == StaticRoleNames.Admin);
            if(admin == null)
            {
                admin = new Role
                {
                    Name = StaticRoleNames.Admin
                };
                _context.Roles.Add(admin);
                _context.SaveChanges();
            }
            var existPermossionNames = _context.RolePermissions.Where(x => x.RoleId == admin.Id).Select(x => x.Name).ToList();
            foreach(var permission in PermissionProvider.Permissions)
            {
                if (!existPermossionNames.Contains(permission.Name))
                {
                    _context.RolePermissions.Add(new RolePermission
                    {
                        RoleId = admin.Id,
                        IsGranted = true,
                        Name = permission.Name,
                    });
                        
                }
                foreach (var chilldPermission in permission.Children)
                {
                    if (!existPermossionNames.Contains(chilldPermission.Name))
                    {
                        _context.RolePermissions.Add(new RolePermission
                        {
                            RoleId = admin.Id,
                            IsGranted = true,
                            Name = chilldPermission.Name,
                        });

                    }

                }
                

            }
            _context.SaveChanges();
        }
    }
}
