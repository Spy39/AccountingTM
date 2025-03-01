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

        public static class TechnicalEquipments
        {
            public const string Default = nameof(TechnicalEquipments);
            public const string Pages = $"{Default}.{PermissionTypes.Pages}";
            public const string Create = $"{Default}.{PermissionTypes.Create}";
            public const string Read = $"{Default}.{PermissionTypes.Read}";
            public const string Delete = $"{Default}.{PermissionTypes.Delete}";
            public const string Archive = $"{Default}.Archive";
        }

        public static class TechnicalEquipmentsInfo
        {
            public const string Default = nameof(TechnicalEquipmentsInfo);
            public const string Pages = $"{Default}.{PermissionTypes.Pages}";
            public const string Create = $"{Default}.{PermissionTypes.Create}";
            public const string Read = $"{Default}.{PermissionTypes.Read}";
            public const string Update = $"{Default}.{PermissionTypes.Update}";
            public const string Delete = $"{Default}.{PermissionTypes.Delete}";
        }

        public static class Archive
        {
            public const string Default = nameof(Archive);
            public const string Pages = $"{Default}.{PermissionTypes.Pages}";
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

        public static class ConsumableHistories
        {
            public const string Default = nameof(ConsumableHistories);
            public const string Pages = $"{Default}.{PermissionTypes.Pages}";
            public const string Create = $"{Default}.{PermissionTypes.Create}";
            public const string Read = $"{Default}.{PermissionTypes.Read}";
            public const string Update = $"{Default}.{PermissionTypes.Update}";
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

        public static class SetsInfo
        {
            public const string Default = nameof(SetsInfo);
            public const string Pages = $"{Default}.{PermissionTypes.Pages}";
            public const string Create = $"{Default}.{PermissionTypes.Create}";
            public const string Read = $"{Default}.{PermissionTypes.Read}";
            public const string Update = $"{Default}.{PermissionTypes.Update}";
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

        public static class ApplicationsInfo
        {
            public const string Default = nameof(ApplicationsInfo);
            public const string Pages = $"{Default}.{PermissionTypes.Pages}";
            public const string Create = $"{Default}.{PermissionTypes.Create}";
            public const string Read = $"{Default}.{PermissionTypes.Read}";
            public const string Update = $"{Default}.{PermissionTypes.Update}";
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

        //Admin

        public static class Directories
        {
            public const string Default = nameof(Directories);
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

        public static class Roles
        {
            public const string Default = nameof(Roles);
            public const string Pages = $"{Default}.{PermissionTypes.Pages}";
            public const string Update = $"{Default}.{PermissionTypes.Update}";
            public const string Create = $"{Default}.{PermissionTypes.Create}";
            public const string Read = $"{Default}.{PermissionTypes.Read}";
            public const string Delete = $"{Default}.{PermissionTypes.Delete}";
        }

        public static class Audit
        {
            public const string Default = nameof(Audit);
            public const string Pages = $"{Default}.{PermissionTypes.Pages}";
            public const string Read = $"{Default}.{PermissionTypes.Read}";
        }
    }
}
