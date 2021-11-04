using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Data.Entity.ModelConfiguration.Configuration;
using App.Models;


namespace App.Data
{
    public class MyDbContext : DbContext
    {
        public DbSet<AgroToxic> AgroToxic { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<Tax> Tax { get; set; }
        public DbSet<Incentive> Incentive { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<Company> Company { get; set; }
        public DbSet<CompanyProduct> CompanyProduct { get; set; }
        public DbSet<CompanyAgrotoxic> CompanyAgrotoxic { get; set; }
        public DbSet<CompanyIncentive> CompanyIncentive { get; set; }
        public DbSet<CompanyTax> CompanyTax { get; set; }

        public MyDbContext(DbContextOptions<MyDbContext> options)
          : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //PRODUCT
            modelBuilder.Entity<CompanyProduct>()
                .HasKey(t => new { t.Company_id, t.Product_id });

            modelBuilder.Entity<CompanyProduct>()
                .HasOne(c => c.Company)
                .WithMany(cp => cp.CompanyProduct)
                .HasForeignKey(c => c.Company_id);

            modelBuilder.Entity<CompanyProduct>()
                 .HasOne(p => p.Product)
                 .WithMany(cp => cp.CompanyProduct)
                 .HasForeignKey(p => p.Product_id);

            //INCENTIVE
            modelBuilder.Entity<CompanyIncentive>()
                .HasKey(t => new { t.Company_id, t.Incentive_id });

            modelBuilder.Entity<CompanyIncentive>()
                .HasOne(c => c.Company)
                .WithMany(cp => cp.CompanyIncentive)
                .HasForeignKey(c => c.Company_id);

            modelBuilder.Entity<CompanyIncentive>()
                 .HasOne(p => p.Incentive)
                 .WithMany(cp => cp.CompanyIncentive)
                 .HasForeignKey(p => p.Incentive_id);

            //AGROTOXIC
            modelBuilder.Entity<CompanyAgrotoxic>()
                .HasKey(t => new { t.Company_id, t.AgroToxic_id });

            modelBuilder.Entity<CompanyAgrotoxic>()
                .HasOne(c => c.Company)
                .WithMany(cp => cp.CompanyAgrotoxic)
                .HasForeignKey(c => c.Company_id);

            modelBuilder.Entity<CompanyAgrotoxic>()
                 .HasOne(p => p.AgroToxic)
                 .WithMany(cp => cp.CompanyAgrotoxic)
                 .HasForeignKey(p => p.AgroToxic_id);

            //TAX
            modelBuilder.Entity<CompanyTax>()
                .HasKey(t => new { t.Company_id, t.Tax_id });

            modelBuilder.Entity<CompanyTax>()
                .HasOne(c => c.Company)
                .WithMany(cp => cp.CompanyTax)
                .HasForeignKey(c => c.Company_id);

            modelBuilder.Entity<CompanyTax>()
                 .HasOne(p => p.Tax)
                 .WithMany(cp => cp.CompanyTax)
                 .HasForeignKey(p => p.Tax_id);
        }
    }
}
