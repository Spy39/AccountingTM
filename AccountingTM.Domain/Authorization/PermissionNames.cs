using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingTM.Domain.Authorization
{
    public static class PermissionTypes
    {
        public const string Pages = nameof(Pages);
        public const string Create = nameof(Create);
        public const string Read = nameof(Read);
        public const string Update = nameof(Update);
        public const string Delete = nameof(Delete);
    }

    public static class PermissionNames
    {
        public static class Roles
        {
            public const string Default = nameof(Roles);
            public const string Pages = $"{Default}.{PermissionTypes.Pages}";
            public const string Update = $"{Default}.{PermissionTypes.Update}";
            public const string Create = $"{Default}.{PermissionTypes.Create}";
            public const string Read = $"{Default}.{PermissionTypes.Read}";
            public const string Delete = $"{Default}.{PermissionTypes.Delete}";
        }

        public static class Users
        {
            public const string Default = nameof(Users);
            public const string Pages = $"{Default}.{PermissionTypes.Pages}";
            public const string Update = $"{Default}.{PermissionTypes.Update}";
            public const string Create = $"{Default}.{PermissionTypes.Create}";
            public const string Read = $"{Default}.{PermissionTypes.Read}";
            public const string Delete = $"{Default}.{PermissionTypes.Delete}";
        }

        public static class TechnicalEquipments
        {
            public const string Default = nameof(TechnicalEquipments);
            public const string Pages = $"{Default}.{PermissionTypes.Pages}";
            public const string Create = $"{Default}.{PermissionTypes.Create}";
            public const string Read = $"{Default}.{PermissionTypes.Read}";
            public const string Delete = $"{Default}.{PermissionTypes.Delete}";
        }
    }
}
