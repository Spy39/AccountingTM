using AccountingTM.Domain.Permissions;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingTM.Domain.Authorization
{
    public class PermissionProvider
    {
        public static IReadOnlyList<Permission> Permissions => _permissions.ToImmutableList();

        private readonly static List<Permission> _permissions = new List<Permission>();

        public static void SetPermissions()
        {
            if (_permissions.Count() != 0)
            {
                return;
            }

            var roles = new Permission(PermissionNames.Roles.Pages, "theater-masks");
            roles.CreateChildPermission(PermissionNames.Roles.Create, "plus");
            roles.CreateChildPermission(PermissionNames.Roles.Read, "book");
            roles.CreateChildPermission(PermissionNames.Roles.Update, "pen");
            roles.CreateChildPermission(PermissionNames.Roles.Delete, "trash");

            var users = new Permission(PermissionNames.Users.Pages, "users");
            users.CreateChildPermission(PermissionNames.Users.Create, "plus");
            users.CreateChildPermission(PermissionNames.Users.Read, "book");
            users.CreateChildPermission(PermissionNames.Users.Update, "pen");
            users.CreateChildPermission(PermissionNames.Users.Delete, "trash");

            var technicalEquipment = new Permission(PermissionNames.TechnicalEquipments.Pages, "user-tie");
            technicalEquipment.CreateChildPermission(PermissionNames.TechnicalEquipments.Create, "plus");
            technicalEquipment.CreateChildPermission(PermissionNames.TechnicalEquipments.Read, "book");
            technicalEquipment.CreateChildPermission(PermissionNames.TechnicalEquipments.Delete, "trash");

            _permissions.Add(roles);
            _permissions.Add(users);
            _permissions.Add(technicalEquipment);
        }
    }
}
