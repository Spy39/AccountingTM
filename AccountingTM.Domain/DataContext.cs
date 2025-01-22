using Accounting.Models;
using AccountingTM.Domain.Models;
using AccountingTM.Domain.Models.Directory;
using AccountingTM.Domain.Models.Tables;
using AccountingTM.Domain.Permissions;
using AccountingTM.Models;
using Microsoft.EntityFrameworkCore;

namespace Accounting.Data
{
    public class DataContext : DbContext
    {
        public DataContext()
        {
        }

        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        }
        //Справочники
        public DbSet<Brand> Brands { get; set; }
		public DbSet<Category> Categories { get; set; }
		public DbSet<Employee> Employees { get; set; }
		public DbSet<Indicator> Indicators { get; set; }
		public DbSet<Location> Locations { get; set; }
        public DbSet<Model> Models { get; set; }
        public DbSet<TypeEquipment> TypeEquipments { get; set; }
		public DbSet<Unit> Units { get; set; }
		public DbSet<TypeConsumable> TypeConsumables { get; set; }
		//Таблицы ТС
		public DbSet<CompletedWork> CompletedWorks { get; set; }
		public DbSet<Conservation> Conservations { get; set; }
		public DbSet<ReceptionAndTransmission> ReceptionAndTransmissions { get; set; }
		public DbSet<Repair> Repairs { get; set; }
		public DbSet<Storage> Storages { get; set; }
        public DbSet<TechnicalEquipmentHistory> TechnicalEquipmentHistories { get; set; }
        //Основные таблицы
        public DbSet<Application> Applications { get; set; }
        public DbSet<ApplicationHistory> ApplicationHistories { get; set; }
        public DbSet<CommentsOnTheApplication> CommentsOnTheApplications { get; set; }
        public DbSet<Characteristic> Characteristics { get; set; }
		public DbSet<Consumable> Consumables { get; set; }
		public DbSet<ConsumableHistory> ConsumableHistories { get; set; }
		public DbSet<Set> Sets { get; set; }
		public DbSet<SetHistory> SetHistories { get; set; }
		public DbSet<Permission> Permissions { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }
        public DbSet<UserPermission> UserPermissions { get; set; }
		public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Role> Roles { get; set; }
		public DbSet<TechnicalEquipment> TechnicalEquipment { get; set; }
		public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
			modelBuilder.Entity<UserRole>().HasKey(x => new { x.RoleId, x.UserId });
			modelBuilder.Entity<UserRole>().HasOne(x => x.Role).WithMany(x => x.UserRoles).HasForeignKey(x => x.RoleId);
            modelBuilder.Entity<UserRole>().HasOne(x => x.User).WithMany(x => x.UserRoles).HasForeignKey(x => x.UserId);
        }
    }
}
