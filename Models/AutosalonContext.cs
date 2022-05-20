using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Autosalon.Models
{
    public partial class AutosalonContext : DbContext
    {
        public AutosalonContext()
        {
        }

        public AutosalonContext(DbContextOptions<AutosalonContext> options)
            : base(options)
        {
        }
        
        public static AutosalonContext GetContext()
        {
            return new AutosalonContext();
        }

        public virtual DbSet<Automobile> Automobiles { get; set; } = null!;
        public virtual DbSet<Customer> Customers { get; set; } = null!;
        public virtual DbSet<Manager> Managers { get; set; } = null!;
        public virtual DbSet<Order> Orders { get; set; } = null!;
        public virtual DbSet<UserAuth> UserAuths { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=LAPTOP-L6440R26;Initial Catalog=Autosalon;Trusted_Connection = True;TrustServerCertificate=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Automobile>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Brand).HasMaxLength(30);

                entity.Property(e => e.Color).HasMaxLength(30);

                entity.Property(e => e.Fuel).HasMaxLength(20);

                entity.Property(e => e.Model).HasMaxLength(30);

                entity.Property(e => e.ReleaseDate)
                    .HasColumnType("date")
                    .HasColumnName("Release_date");
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.AuthId).HasColumnName("Auth_id");

                entity.Property(e => e.Name).HasMaxLength(30);

                entity.Property(e => e.Surname).HasMaxLength(30);

                entity.HasOne(d => d.Auth)
                    .WithMany(p => p.Customers)
                    .HasForeignKey(d => d.AuthId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Customers_Auth_id_fk");
            });

            modelBuilder.Entity<Manager>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.AmountOfSaledCars).HasColumnName("Amount_of_saled_cars");

                entity.Property(e => e.AuthId).HasColumnName("Auth_id");

                entity.Property(e => e.Name).HasMaxLength(30);

                entity.Property(e => e.Surname).HasMaxLength(30);

                entity.HasOne(d => d.Auth)
                    .WithMany(p => p.Managers)
                    .HasForeignKey(d => d.AuthId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Managers_Auth_id_fk");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.AutomobileId).HasColumnName("Automobile_id");

                entity.Property(e => e.CustomerId).HasColumnName("Customer_id");

                entity.Property(e => e.Date).HasColumnType("date");

                entity.Property(e => e.ManagerId).HasColumnName("Manager_id");

                entity.Property(e => e.Status).HasMaxLength(15);

                entity.Property(e => e.TotalPrice).HasColumnName("Total_price");

                entity.HasOne(d => d.Automobile)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.AutomobileId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Orders_Automobiles_Id_fk");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Orders_Customers_Id_fk");

                entity.HasOne(d => d.Manager)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.ManagerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Orders_Managers_Id_fk");
            });

            modelBuilder.Entity<UserAuth>(entity =>
            {
                entity.ToTable("User_Auth");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Email).HasMaxLength(40);

                entity.Property(e => e.Password).HasMaxLength(30);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
