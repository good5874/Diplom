namespace WebApplication1.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=Model1")
        {
        }

        public virtual DbSet<AspNetRoleClaims> AspNetRoleClaims { get; set; }
        public virtual DbSet<AspNetRoles> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaims> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogins> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUsers> AspNetUsers { get; set; }
        public virtual DbSet<AspNetUserTokens> AspNetUserTokens { get; set; }
        public virtual DbSet<Cargos> Cargos { get; set; }
        public virtual DbSet<Cars> Cars { get; set; }
        public virtual DbSet<Customers> Customers { get; set; }
        public virtual DbSet<Drivers> Drivers { get; set; }
        public virtual DbSet<Employees> Employees { get; set; }
        public virtual DbSet<Managers> Managers { get; set; }
        public virtual DbSet<Orders> Orders { get; set; }
        public virtual DbSet<Organizations> Organizations { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AspNetRoles>()
                .HasMany(e => e.AspNetRoleClaims)
                .WithRequired(e => e.AspNetRoles)
                .HasForeignKey(e => e.RoleId);

            modelBuilder.Entity<AspNetRoles>()
                .HasMany(e => e.AspNetUsers)
                .WithMany(e => e.AspNetRoles)
                .Map(m => m.ToTable("AspNetUserRoles").MapLeftKey("RoleId").MapRightKey("UserId"));

            modelBuilder.Entity<AspNetUsers>()
                .HasMany(e => e.AspNetUserClaims)
                .WithRequired(e => e.AspNetUsers)
                .HasForeignKey(e => e.UserId);

            modelBuilder.Entity<AspNetUsers>()
                .HasMany(e => e.AspNetUserLogins)
                .WithRequired(e => e.AspNetUsers)
                .HasForeignKey(e => e.UserId);

            modelBuilder.Entity<AspNetUsers>()
                .HasMany(e => e.AspNetUserTokens)
                .WithRequired(e => e.AspNetUsers)
                .HasForeignKey(e => e.UserId);

            modelBuilder.Entity<AspNetUsers>()
                .HasOptional(e => e.Employees)
                .WithRequired(e => e.AspNetUsers);

            modelBuilder.Entity<Customers>()
                .HasOptional(e => e.AspNetUsers)
                .WithRequired(e => e.Customers);

            modelBuilder.Entity<Customers>()
                .HasMany(e => e.Cargos)
                .WithRequired(e => e.Customers)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Customers>()
                .HasMany(e => e.Orders)
                .WithRequired(e => e.Customers)
                .HasForeignKey(e => e.CustomerId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Drivers>()
                .HasOptional(e => e.Employees)
                .WithRequired(e => e.Drivers);

            modelBuilder.Entity<Managers>()
                .HasOptional(e => e.Employees)
                .WithRequired(e => e.Managers);

            modelBuilder.Entity<Managers>()
                .HasMany(e => e.Orders)
                .WithOptional(e => e.Managers)
                .HasForeignKey(e => e.ManagerId);

            modelBuilder.Entity<Orders>()
                .HasMany(e => e.Cargos)
                .WithOptional(e => e.Orders)
                .HasForeignKey(e => e.OrderId);

            modelBuilder.Entity<Organizations>()
                .HasMany(e => e.Cars)
                .WithRequired(e => e.Organizations)
                .HasForeignKey(e => e.OrganizationId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Organizations>()
                .HasMany(e => e.Employees)
                .WithRequired(e => e.Organizations)
                .HasForeignKey(e => e.OrganizationId)
                .WillCascadeOnDelete(false);
        }
    }
}
