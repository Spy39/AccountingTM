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

            var technicalEquipments = new Permission(PermissionNames.TechnicalEquipments.Pages, "user-tie");
            technicalEquipments.CreateChildPermission(PermissionNames.TechnicalEquipments.Create, "plus");
            technicalEquipments.CreateChildPermission(PermissionNames.TechnicalEquipments.Read, "book");
            technicalEquipments.CreateChildPermission(PermissionNames.TechnicalEquipments.Delete, "trash");
            technicalEquipments.CreateChildPermission(PermissionNames.TechnicalEquipments.Archive, "box-archive");

            var technicalEquipmentsInfo = new Permission(PermissionNames.TechnicalEquipmentsInfo.Pages, "user-tie");
            technicalEquipmentsInfo.CreateChildPermission(PermissionNames.TechnicalEquipmentsInfo.Create, "plus");
            technicalEquipmentsInfo.CreateChildPermission(PermissionNames.TechnicalEquipmentsInfo.Read, "book");
            technicalEquipmentsInfo.CreateChildPermission(PermissionNames.TechnicalEquipmentsInfo.Update, "pen");
            technicalEquipmentsInfo.CreateChildPermission(PermissionNames.TechnicalEquipmentsInfo.Delete, "trash");

            var archive = new Permission(PermissionNames.Archive.Pages, "user-tie");
            archive.CreateChildPermission(PermissionNames.Archive.Read, "book");
            archive.CreateChildPermission(PermissionNames.Archive.Delete, "trash");

            var consumables = new Permission(PermissionNames.Consumables.Pages, "user-tie");
            consumables.CreateChildPermission(PermissionNames.Consumables.Create, "plus");
            consumables.CreateChildPermission(PermissionNames.Consumables.Read, "book");
            consumables.CreateChildPermission(PermissionNames.Consumables.Delete, "trash");

            var consumableHistories = new Permission(PermissionNames.ConsumableHistories.Pages, "user-tie");
            consumableHistories.CreateChildPermission(PermissionNames.ConsumableHistories.Create, "plus");
            consumableHistories.CreateChildPermission(PermissionNames.ConsumableHistories.Read, "book");
            consumableHistories.CreateChildPermission(PermissionNames.ConsumableHistories.Update, "pen");
            consumableHistories.CreateChildPermission(PermissionNames.ConsumableHistories.Delete, "trash");

            var sets = new Permission(PermissionNames.Sets.Pages, "user-tie");
            sets.CreateChildPermission(PermissionNames.Sets.Create, "plus");
            sets.CreateChildPermission(PermissionNames.Sets.Read, "book");
            sets.CreateChildPermission(PermissionNames.Sets.Delete, "trash");

            var setsInfo = new Permission(PermissionNames.SetsInfo.Pages, "user-tie");
            setsInfo.CreateChildPermission(PermissionNames.SetsInfo.Create, "plus");
            setsInfo.CreateChildPermission(PermissionNames.SetsInfo.Read, "book");
            setsInfo.CreateChildPermission(PermissionNames.SetsInfo.Update, "pen");
            setsInfo.CreateChildPermission(PermissionNames.SetsInfo.Delete, "trash");

            var applications = new Permission(PermissionNames.Applications.Pages, "user-tie");
            applications.CreateChildPermission(PermissionNames.Applications.Create, "plus");
            applications.CreateChildPermission(PermissionNames.Applications.Read, "book");
            applications.CreateChildPermission(PermissionNames.Applications.Delete, "trash");

            var applicationsInfo = new Permission(PermissionNames.ApplicationsInfo.Pages, "user-tie");
            applicationsInfo.CreateChildPermission(PermissionNames.ApplicationsInfo.Create, "plus");
            applicationsInfo.CreateChildPermission(PermissionNames.ApplicationsInfo.Read, "book");
            applicationsInfo.CreateChildPermission(PermissionNames.ApplicationsInfo.Update, "pen");
            applicationsInfo.CreateChildPermission(PermissionNames.ApplicationsInfo.Delete, "trash");

            var analysis = new Permission(PermissionNames.Analysis.Pages, "user-tie");
            analysis.CreateChildPermission(PermissionNames.Analysis.Create, "plus"); //Выбрать фильтр
            analysis.CreateChildPermission(PermissionNames.Analysis.Read, "book");

            var statistics = new Permission(PermissionNames.Statistics.Pages, "user-tie");
            statistics.CreateChildPermission(PermissionNames.Statistics.Create, "plus"); //Выбрать фильтр
            statistics.CreateChildPermission(PermissionNames.Statistics.Read, "book");

            //Admin

            var directories = new Permission(PermissionNames.Directories.Pages, "user-tie");
            directories.CreateChildPermission(PermissionNames.Directories.Create, "plus");
            directories.CreateChildPermission(PermissionNames.Directories.Read, "book");
            directories.CreateChildPermission(PermissionNames.Directories.Update, "pen");
            directories.CreateChildPermission(PermissionNames.Directories.Delete, "trash");

            var users = new Permission(PermissionNames.Users.Pages, "user-tie");
            users.CreateChildPermission(PermissionNames.Users.Create, "plus");
            users.CreateChildPermission(PermissionNames.Users.Read, "book");
            users.CreateChildPermission(PermissionNames.Users.Update, "pen");
            users.CreateChildPermission(PermissionNames.Users.Delete, "trash");

            var roles = new Permission(PermissionNames.Roles.Pages, "roles");
            roles.CreateChildPermission(PermissionNames.Roles.Create, "plus");
            roles.CreateChildPermission(PermissionNames.Roles.Read, "book");
            roles.CreateChildPermission(PermissionNames.Roles.Update, "pen");
            roles.CreateChildPermission(PermissionNames.Roles.Delete, "trash");

            var audit = new Permission(PermissionNames.Audit.Pages, "users");
            audit.CreateChildPermission(PermissionNames.Audit.Read, "book");

            _permissions.Add(technicalEquipments);
            _permissions.Add(technicalEquipmentsInfo);
            _permissions.Add(archive);
            _permissions.Add(consumables);
            _permissions.Add(consumableHistories);
            _permissions.Add(sets);
            _permissions.Add(setsInfo);
            _permissions.Add(applications);
            _permissions.Add(applicationsInfo);
            _permissions.Add(analysis);
            _permissions.Add(statistics);
            
            _permissions.Add(directories);
            _permissions.Add(users);
            _permissions.Add(roles);
            _permissions.Add(audit);
        }
    }
}
