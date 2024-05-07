using Accounting.Models;
using AccountingTM.Domain.Models;
using AccountingTM.Domain.Models.Directory;
using AccountingTM.Domain.Models.Tables;
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
		public DbSet<TypeEquipment> TypeEquipments { get; set; }
		public DbSet<Unit> Units { get; set; }
		//Таблицы ТС
		public DbSet<CompletedWork> CompletedWorks { get; set; }
		public DbSet<Conservation> Conservations { get; set; }
		public DbSet<DisposalInformation> DisposalInformations { get; set; }
		public DbSet<ReceptionAndTransmission> ReceptionAndTransmissions { get; set; }
		public DbSet<Repair> Repairs { get; set; }
		public DbSet<Storage> Storages { get; set; }
		//Основные таблицы
		public DbSet<Application> Applications { get; set; }
		public DbSet<Characteristics> Characteristics { get; set; }
		public DbSet<DocumentType> DocumentType { get; set; }
        public DbSet<Malfunction> Malfunction { get; set; }
		public DbSet<Moving> Moving { get; set; }
		public DbSet<Rights> Rights { get; set; }
		public DbSet<Roles> Roles { get; set; }
		public DbSet<TechnicalEquipment> TechnicalEquipment { get; set; }
		public DbSet<User> Users { get; set; }
	}
}
