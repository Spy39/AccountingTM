using AccountingTM.Domain.Permissions;
using System.Collections.Immutable;

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

            var technicalEquipments = new Permission(PermissionNames.TechnicalEquipments.Pages, "user-tie");
            technicalEquipments.CreateChildPermission(PermissionNames.TechnicalEquipments.Create, "plus");
            technicalEquipments.CreateChildPermission(PermissionNames.TechnicalEquipments.Read, "book");
            technicalEquipments.CreateChildPermission(PermissionNames.TechnicalEquipments.Delete, "trash");

            var consumables = new Permission(PermissionNames.TechnicalEquipments.Pages, "user-tie");
            consumables.CreateChildPermission(PermissionNames.TechnicalEquipments.Create, "plus");
            consumables.CreateChildPermission(PermissionNames.TechnicalEquipments.Read, "book");
            consumables.CreateChildPermission(PermissionNames.TechnicalEquipments.Delete, "trash");

            var sets = new Permission(PermissionNames.TechnicalEquipments.Pages, "user-tie");
            sets.CreateChildPermission(PermissionNames.TechnicalEquipments.Create, "plus");
            sets.CreateChildPermission(PermissionNames.TechnicalEquipments.Read, "book");
            sets.CreateChildPermission(PermissionNames.TechnicalEquipments.Delete, "trash");

            var applications = new Permission(PermissionNames.TechnicalEquipments.Pages, "user-tie");
            applications.CreateChildPermission(PermissionNames.TechnicalEquipments.Create, "plus");
            applications.CreateChildPermission(PermissionNames.TechnicalEquipments.Read, "book");
            applications.CreateChildPermission(PermissionNames.TechnicalEquipments.Delete, "trash");

            var analysis = new Permission(PermissionNames.TechnicalEquipments.Pages, "user-tie");
            analysis.CreateChildPermission(PermissionNames.TechnicalEquipments.Create, "plus");
            analysis.CreateChildPermission(PermissionNames.TechnicalEquipments.Read, "book");
            analysis.CreateChildPermission(PermissionNames.TechnicalEquipments.Delete, "trash");

            var statistics = new Permission(PermissionNames.TechnicalEquipments.Pages, "user-tie");
            statistics.CreateChildPermission(PermissionNames.TechnicalEquipments.Create, "plus");
            statistics.CreateChildPermission(PermissionNames.TechnicalEquipments.Read, "book");
            statistics.CreateChildPermission(PermissionNames.TechnicalEquipments.Delete, "trash");

            var administrations = new Permission(PermissionNames.Users.Pages, "users");
            administrations.CreateChildPermission(PermissionNames.Users.Create, "plus");
            administrations.CreateChildPermission(PermissionNames.Users.Read, "book");
            administrations.CreateChildPermission(PermissionNames.Users.Update, "pen");
            administrations.CreateChildPermission(PermissionNames.Users.Delete, "trash");

            var directories = new Permission(PermissionNames.Users.Pages, "users");
            directories.CreateChildPermission(PermissionNames.Users.Create, "plus");
            directories.CreateChildPermission(PermissionNames.Users.Read, "book");
            directories.CreateChildPermission(PermissionNames.Users.Update, "pen");
            directories.CreateChildPermission(PermissionNames.Users.Delete, "trash");

            _permissions.Add(roles);
            _permissions.Add(users);
            _permissions.Add(technicalEquipments);
            _permissions.Add(consumables);
            _permissions.Add(sets);
            _permissions.Add(applications);
            _permissions.Add(analysis);
            _permissions.Add(statistics);
            _permissions.Add(administrations);
            _permissions.Add(directories);
        }
    }
}
