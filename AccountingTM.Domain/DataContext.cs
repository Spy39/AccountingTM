﻿using Accounting.Models;
using AccountingTM.Domain.Models;
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
        }

        public DbSet<User> Users { get; set; }
        public DbSet<TechnicalEquipment> TechnicalEquipment { get; set; }
        public DbSet<Staff> Staff { get; set; }
        public DbSet<Moving> Moving { get; set; }
        public DbSet<Malfunction> Malfunction { get; set; }
        public DbSet<DocumentType> DocumentType { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseUseNpgsql("User ID=postgres;Password=123qwe;Host=localhost;Port=5435;Database=accountingtm;Pooling=true;");
        }
    }
}
