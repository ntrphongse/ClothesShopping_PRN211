using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace DTOLibrary
{
    public partial class lPVNgP26wKContext : DbContext
    {
        public lPVNgP26wKContext()
        {
        }

        public lPVNgP26wKContext(DbContextOptions<lPVNgP26wKContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Product> Products { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                string strConnection = MyConnection.GetConnectionString();
                optionsBuilder.UseMySQL(strConnection);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("Product");

                entity.HasComment("Product Information");

                entity.HasIndex(e => e.CategoryId, "CategoryFK");

                entity.Property(e => e.ProductId).HasColumnType("int(11)");

                entity.Property(e => e.CategoryId).HasColumnType("int(11)");

                entity.Property(e => e.Image)
                    .IsRequired()
                    .HasMaxLength(1000);

                entity.Property(e => e.ProductName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Quantity).HasColumnType("int(11)");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
