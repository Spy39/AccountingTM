namespace AccountingTM.Domain.Authorization
{
    public static class PermissionTypes
    {
        public const string Pages = nameof(Pages);   //Открыть страницу
        public const string Create = nameof(Create); //Изменить
        public const string Read = nameof(Read);     //Создать
        public const string Update = nameof(Update); //Прочитать
        public const string Delete = nameof(Delete); //Удалить
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

        public static class Consumables
        {
            public const string Default = nameof(Consumables);
            public const string Pages = $"{Default}.{PermissionTypes.Pages}";
            public const string Create = $"{Default}.{PermissionTypes.Create}";
            public const string Read = $"{Default}.{PermissionTypes.Read}";
            public const string Delete = $"{Default}.{PermissionTypes.Delete}";
        }

        public static class Sets
        {
            public const string Default = nameof(Sets);
            public const string Pages = $"{Default}.{PermissionTypes.Pages}";
            public const string Create = $"{Default}.{PermissionTypes.Create}";
            public const string Read = $"{Default}.{PermissionTypes.Read}";
            public const string Delete = $"{Default}.{PermissionTypes.Delete}";
        }

        public static class Applications
        {
            public const string Default = nameof(Applications);
            public const string Pages = $"{Default}.{PermissionTypes.Pages}";
            public const string Create = $"{Default}.{PermissionTypes.Create}";
            public const string Read = $"{Default}.{PermissionTypes.Read}";
            public const string Delete = $"{Default}.{PermissionTypes.Delete}";
        }

        public static class Analysis
        {
            public const string Default = nameof(Analysis);
            public const string Pages = $"{Default}.{PermissionTypes.Pages}";
            public const string Create = $"{Default}.{PermissionTypes.Create}";
            public const string Read = $"{Default}.{PermissionTypes.Read}";
        }

        public static class Statistics
        {
            public const string Default = nameof(Statistics);
            public const string Pages = $"{Default}.{PermissionTypes.Pages}";
            public const string Create = $"{Default}.{PermissionTypes.Create}";
            public const string Read = $"{Default}.{PermissionTypes.Read}";
        }

        public static class Administrations
        {
            public const string Default = nameof(Administrations);
            public const string Pages = $"{Default}.{PermissionTypes.Pages}";
            public const string Update = $"{Default}.{PermissionTypes.Update}";
            public const string Create = $"{Default}.{PermissionTypes.Create}";
            public const string Read = $"{Default}.{PermissionTypes.Read}";
            public const string Delete = $"{Default}.{PermissionTypes.Delete}";
        }

        public static class Directories
        {
            public const string Default = nameof(Directories);
            public const string Pages = $"{Default}.{PermissionTypes.Pages}";
            public const string Update = $"{Default}.{PermissionTypes.Update}";
            public const string Create = $"{Default}.{PermissionTypes.Create}";
            public const string Read = $"{Default}.{PermissionTypes.Read}";
            public const string Delete = $"{Default}.{PermissionTypes.Delete}";
        }
    }
}
