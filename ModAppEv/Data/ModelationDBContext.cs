using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using ModAppEv.Models;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace ModAppEv.Data;

public partial class ModelationDBContext : DbContext
{
    public ModelationDBContext()
    {
    }

    public ModelationDBContext(DbContextOptions<ModelationDBContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<ProductChangeLog> ProductChangeLogs { get; set; }

    public virtual DbSet<Supplier> Suppliers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("server=localhost;user=root;password=M226PEas05_12;database=app_modelation", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.35-mysql"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("category");

            entity.HasIndex(e => e.Name, "name_UNIQUE").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Description)
                .HasMaxLength(60)
                .HasColumnName("description");
            entity.Property(e => e.Name)
                .HasMaxLength(45)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("product");

            entity.HasIndex(e => e.Category, "fk_category_idx");

            entity.HasIndex(e => e.SupplierId, "fk_supplier_id_idx");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Category)
                .HasMaxLength(45)
                .HasColumnName("category");
            entity.Property(e => e.Discount).HasColumnName("discount");
            entity.Property(e => e.Name)
                .HasMaxLength(45)
                .HasColumnName("name");
            entity.Property(e => e.Price).HasColumnName("price");
            entity.Property(e => e.Stock).HasColumnName("stock");
            entity.Property(e => e.SupplierId).HasColumnName("supplier_id");

            entity.HasOne(d => d.CategoryNavigation).WithMany(p => p.Products)
                .HasPrincipalKey(p => p.Name)
                .HasForeignKey(d => d.Category)
                .HasConstraintName("fk_category");

            entity.HasOne(d => d.Supplier).WithMany(p => p.Products)
                .HasForeignKey(d => d.SupplierId)
                .HasConstraintName("fk_supplier_id");
        });

        modelBuilder.Entity<ProductChangeLog>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("product_change_log");

            entity.HasIndex(e => e.ProductId, "fk_product_id_idx");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ChangeBy)
                .HasMaxLength(45)
                .HasColumnName("change_by");
            entity.Property(e => e.ChangeDate)
                .HasColumnType("datetime")
                .HasColumnName("change_date");
            entity.Property(e => e.ProductId).HasColumnName("product_id");
            entity.Property(e => e.TableAction)
                .HasColumnType("enum('Update','Delete','Post')")
                .HasColumnName("table_action");

            entity.HasOne(d => d.Product).WithMany(p => p.ProductChangeLogs)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_product_id");
        });

        modelBuilder.Entity<Supplier>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("supplier");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Address)
                .HasMaxLength(60)
                .HasColumnName("address");
            entity.Property(e => e.Name)
                .HasMaxLength(45)
                .HasColumnName("name");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(10)
                .HasColumnName("phone_number");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
