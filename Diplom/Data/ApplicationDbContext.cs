using System;
using System.Collections.Generic;
using System.Text;
using Diplom.Models.Tables;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Diplom.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
        public new virtual DbSet<Users> Users { get; set; }
        public virtual DbSet<CarOrder> CarOrder { get; set; }
        public virtual DbSet<Cargos> Cargos { get; set; }
        public virtual DbSet<Cars> Cars { get; set; }
        public virtual DbSet<Customers> Customers { get; set; }
        public virtual DbSet<DriverOrder> DriverOrder { get; set; }
        public virtual DbSet<Drivers> Drivers { get; set; }
        public virtual DbSet<Employees> Employees { get; set; }
        public virtual DbSet<Managers> Managers { get; set; }
        public virtual DbSet<Orders> Orders { get; set; }
        public virtual DbSet<Organizations> Organizations { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=Diplom1;Trusted_Connection=False;MultipleActiveResultSets=true;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<CarOrder>(entity =>
            {
                entity.HasNoKey();

                entity.HasOne(d => d.Car)
                    .WithMany()
                    .HasForeignKey(d => d.CarId)
                    .HasConstraintName("FK_CarOrder_Cars");

                entity.HasOne(d => d.Order)
                    .WithMany()
                    .HasForeignKey(d => d.OrderId)
                    .HasConstraintName("FK_CarOrder_Orders");
            });

            modelBuilder.Entity<Cargos>(entity =>
            {
                entity.Property(e => e.CustomersId)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.Property(e => e.LocationLoading).IsRequired();

                entity.Property(e => e.LocationUnloading).IsRequired();

                entity.Property(e => e.Status).IsRequired();

                entity.HasOne(d => d.Customers)
                    .WithMany(p => p.Cargos)
                    .HasForeignKey(d => d.CustomersId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Cargos_Customers1");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.Cargos)
                    .HasForeignKey(d => d.OrderId)
                    .HasConstraintName("FK_Cargos_Orders");
            });

            modelBuilder.Entity<Cars>(entity =>
            {
                entity.Property(e => e.CarBrand)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.StateNamber)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.Organization)
                    .WithMany(p => p.Cars)
                    .HasForeignKey(d => d.OrganizationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Cars_Organizations");
            });

            modelBuilder.Entity<Customers>(entity =>
            {
                entity.Property(e => e.PasportData)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.IdNavigation)
                    .WithOne(p => p.Customers)
                    .HasForeignKey<Customers>(d => d.Id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Customers_AspNetUsers");
            });

            modelBuilder.Entity<DriverOrder>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.DriverId).HasMaxLength(450);

                entity.HasOne(d => d.Driver)
                    .WithMany()
                    .HasForeignKey(d => d.DriverId)
                    .HasConstraintName("FK_DriverOrder_Drivers");

                entity.HasOne(d => d.Order)
                    .WithMany()
                    .HasForeignKey(d => d.OrderId)
                    .HasConstraintName("FK_DriverOrder_Orders");
            });

            modelBuilder.Entity<Drivers>(entity =>
            {
                entity.Property(e => e.DrivingLicense)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.Employees)
                    .WithOne(p => p.Driver)
                    .HasForeignKey<Drivers>(d => d.Id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Drivers_Employees");
            });

            modelBuilder.Entity<Employees>(entity =>
            {
                entity.HasIndex(e => e.OrganizationId);

                entity.Property(e => e.Address).IsRequired();

                entity.Property(e => e.DateOfBirth).HasColumnType("datetime");

                entity.Property(e => e.EmploymentDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.PasportData)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.User)
                    .WithOne(p => p.Employees)
                    .HasForeignKey<Employees>(d => d.Id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Employees_AspNetUsers");

                entity.HasOne(d => d.Organization)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.OrganizationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Employees_Organizations1");
            });

            modelBuilder.Entity<Managers>(entity =>
            {
                entity.HasOne(d => d.Employees)
                    .WithOne(p => p.Manager)
                    .HasForeignKey<Managers>(d => d.Id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Managers_Employees");
            });

            modelBuilder.Entity<Orders>(entity =>
            {
                entity.Property(e => e.CustomerId)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.Property(e => e.DateOfTheContract)
                    .IsRequired()
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ManagerId).HasMaxLength(450);

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Orders_Customers");

                entity.HasOne(d => d.Manager)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.ManagerId)
                    .HasConstraintName("FK_Orders_Managers");
            });

            modelBuilder.Entity<Organizations>(entity =>
            {
                entity.Property(e => e.Address).IsRequired();

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.PhoneNamber)
                    .IsRequired()
                    .HasMaxLength(50);
            });
        }
    }

}
