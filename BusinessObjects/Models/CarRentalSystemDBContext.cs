using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

#nullable disable

namespace BusinessObjects.Models
{
    public partial class CarRentalSystemDBContext : DbContext
    {

        //read class appsettings.json

        public string GetConnectionString()
        {
            IConfiguration config = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", true, true).Build();
            var strConn = config["ConnectionStrings:DB"];
            return strConn;
        }

        //get Account Admin
        public StaffAccount getDefaultAccount()
        {
            var admin = new StaffAccount();
            IConfiguration config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true)
                .Build();
            admin.StaffId = config["DefaultAccount:Id"];
            admin.FullName = config["DefaultAccount:FullName"];
            admin.Email = config["DefaultAccount:Email"];
            admin.Password = config["DefaultAccount:Password"];
            admin.Role = 0;
            return admin;
        }

        public CarRentalSystemDBContext()
        {
        }

        public CarRentalSystemDBContext(DbContextOptions<CarRentalSystemDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Car> Cars { get; set; }
        public virtual DbSet<CarProducer> CarProducers { get; set; }
        public virtual DbSet<CarRental> CarRentals { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Review> Reviews { get; set; }
        public virtual DbSet<StaffAccount> StaffAccounts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                //optionsBuilder.UseSqlServer("server =(local); database=CarRentalSystemDB;uid=sa;pwd=1801;TrustServerCertificate=True");
                optionsBuilder.UseSqlServer(GetConnectionString());
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Car>(entity =>
            {
                entity.ToTable("Car");

                entity.Property(e => e.CarId)
                    .HasMaxLength(25)
                    .HasColumnName("CarID");

                entity.Property(e => e.CarName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Color)
                    .IsRequired()
                    .HasMaxLength(25);

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(e => e.ImportDate).HasColumnType("datetime");

                entity.Property(e => e.ProducerId)
                    .HasMaxLength(25)
                    .HasColumnName("ProducerID");

                entity.Property(e => e.RentPrice).HasColumnType("decimal(18, 0)");

                entity.HasOne(d => d.Producer)
                    .WithMany(p => p.Cars)
                    .HasForeignKey(d => d.ProducerId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__Car__ProducerID__38996AB5");
            });

            modelBuilder.Entity<CarProducer>(entity =>
            {
                entity.HasKey(e => e.ProducerId)
                    .HasName("PK__CarProdu__133696B24D827D40");

                entity.ToTable("CarProducer");

                entity.Property(e => e.ProducerId)
                    .HasMaxLength(25)
                    .HasColumnName("ProducerID");

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Country)
                    .IsRequired()
                    .HasMaxLength(25);

                entity.Property(e => e.ProcuderName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<CarRental>(entity =>
            {
                entity.HasKey(e => new { e.CustomerId, e.CarId, e.PickupDate, e.ReturnDate })
                    .HasName("PK__CarRenta__E01D1225ECD03B36");

                entity.ToTable("CarRental");

                entity.Property(e => e.CustomerId)
                    .HasMaxLength(25)
                    .HasColumnName("CustomerID");

                entity.Property(e => e.CarId)
                    .HasMaxLength(25)
                    .HasColumnName("CarID");

                entity.Property(e => e.PickupDate).HasColumnType("datetime");

                entity.Property(e => e.ReturnDate).HasColumnType("datetime");

                entity.Property(e => e.RentPrice).HasColumnType("decimal(18, 0)");

                entity.HasOne(d => d.Car)
                    .WithMany(p => p.CarRentals)
                    .HasForeignKey(d => d.CarId)
                    .HasConstraintName("FK__CarRental__CarID__3E52440B");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.CarRentals)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("FK__CarRental__Custo__3D5E1FD2");
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.ToTable("Customer");

                entity.Property(e => e.CustomerId)
                    .HasMaxLength(25)
                    .HasColumnName("CustomerID");

                entity.Property(e => e.CustomerEmail)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.CustomerName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.CustomerPassword)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.IdentityCard)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.LicenceDate).HasColumnType("datetime");

                entity.Property(e => e.LicenceNumber)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Mobile)
                    .IsRequired()
                    .HasMaxLength(20);
            });

            modelBuilder.Entity<Review>(entity =>
            {
                entity.HasKey(e => new { e.CustomerId, e.CarId })
                    .HasName("PK__Review__522467F8EE1E02BF");

                entity.ToTable("Review");

                entity.Property(e => e.CustomerId)
                    .HasMaxLength(25)
                    .HasColumnName("CustomerID");

                entity.Property(e => e.CarId)
                    .HasMaxLength(25)
                    .HasColumnName("CarID");

                entity.Property(e => e.Comment).HasMaxLength(250);

                entity.HasOne(d => d.Car)
                    .WithMany(p => p.Reviews)
                    .HasForeignKey(d => d.CarId)
                    .HasConstraintName("FK__Review__CarID__4222D4EF");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Reviews)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("FK__Review__Customer__412EB0B6");
            });

            modelBuilder.Entity<StaffAccount>(entity =>
            {
                entity.HasKey(e => e.StaffId)
                    .HasName("PK__StaffAcc__96D4AAF7913DA7CA");

                entity.ToTable("StaffAccount");

                entity.Property(e => e.StaffId)
                    .HasMaxLength(25)
                    .HasColumnName("StaffID");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(e => e.FullName)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(e => e.Password).HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
