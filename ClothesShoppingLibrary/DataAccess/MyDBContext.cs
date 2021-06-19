using System;
using System.IO;
using ClothesShoppingLibrary.DataAccess.Category;
using ClothesShoppingLibrary.DataAccess.Order;
using ClothesShoppingLibrary.DataAccess.OrderDetail;
using ClothesShoppingLibrary.DataAccess.Product;
using ClothesShoppingLibrary.DataAccess.Role;
using ClothesShoppingLibrary.DataAccess.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

#nullable disable

namespace ClothesShoppingLibrary.DataAccess
{
    public partial class MyDBContext : DbContext
    {
        public MyDBContext()
        {
        }

        public MyDBContext(DbContextOptions<MyDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<CategoryDTO> Categories { get; set; }
        public virtual DbSet<OrderDTO> Orders { get; set; }
        public virtual DbSet<OrderDetailDTO> OrderDetails { get; set; }
        public virtual DbSet<ProductDTO> Products { get; set; }
        public virtual DbSet<RoleDTO> Roles { get; set; }
        public virtual DbSet<UserDTO> Users { get; set; }

        private string GetConnectionString()
        {
            IConfiguration config = new ConfigurationBuilder()
                                    .SetBasePath(Directory.GetCurrentDirectory())
                                    .AddJsonFile("appsettings.json", true, true)
                                    .Build();
            var strConnection = config["ConnectionString:ClothesShoppingDB"];
            return strConnection;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                string connectionString = GetConnectionString();
                optionsBuilder.UseMySQL(connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CategoryDTO>(entity =>
            {
                entity.ToTable("Category");

                entity.HasComment("Category Information");

                entity.Property(e => e.CategoryId).HasColumnType("int(11)");

                entity.Property(e => e.CategoryName)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<OrderDTO>(entity =>
            {
                entity.HasComment("Orders Summary");

                entity.HasIndex(e => e.CustomerId, "UserFK");

                entity.Property(e => e.OrderId).HasColumnType("int(11)");

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasColumnType("varchar(10000)");

                entity.Property(e => e.CustomerId).HasColumnType("int(11)");

                entity.Property(e => e.NumberOfItem).HasColumnType("int(11)");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("UserFK");
            });

            modelBuilder.Entity<OrderDetailDTO>(entity =>
            {
                entity.ToTable("OrderDetail");

                entity.HasComment("Order's Detail");

                entity.HasIndex(e => e.OrderId, "OrderFK");

                entity.HasIndex(e => e.ProductId, "ProductFK");

                entity.Property(e => e.OrderDetailId).HasColumnType("int(11)");

                entity.Property(e => e.OrderId).HasColumnType("int(11)");

                entity.Property(e => e.ProductId).HasColumnType("int(11)");

                entity.Property(e => e.Quantity).HasColumnType("int(11)");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("OrderFK");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("ProductFK");
            });

            modelBuilder.Entity<ProductDTO>(entity =>
            {
                entity.HasKey(e => e.Productd)
                    .HasName("PRIMARY");

                entity.ToTable("Product");

                entity.HasComment("Product Information");

                entity.HasIndex(e => e.CategoryId, "CategoryFK");

                entity.Property(e => e.Productd).HasColumnType("int(11)");

                entity.Property(e => e.CategoryId).HasColumnType("int(11)");

                entity.Property(e => e.Image)
                    .IsRequired()
                    .HasMaxLength(1000);

                entity.Property(e => e.ProductName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Quantity).HasColumnType("int(11)");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("CategoryFK");
            });

            modelBuilder.Entity<RoleDTO>(entity =>
            {
                entity.ToTable("Role");

                entity.HasComment("Role Information");

                entity.Property(e => e.RoleId).HasColumnType("int(11)");

                entity.Property(e => e.RoleName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<UserDTO>(entity =>
            {
                entity.ToTable("User");

                entity.HasComment("User Information");

                entity.HasIndex(e => e.Role, "RoleFK");

                entity.Property(e => e.UserId).HasColumnType("int(11)");

                entity.Property(e => e.Address).HasColumnType("varchar(10000)");

                entity.Property(e => e.Birthday).HasColumnType("date");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.FullName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.PhoneNumber).HasMaxLength(20);

                entity.Property(e => e.Role).HasColumnType("int(11)");

                entity.HasOne(d => d.RoleNavigation)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.Role)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("RoleFK");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
