using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace Demop.Entities;

public partial class Db10Context : DbContext
{
    public Db10Context()
    {
    }

    public Db10Context(DbContextOptions<Db10Context> options)
        : base(options)
    {
    }

    public virtual DbSet<Address> Addresses { get; set; }

    public virtual DbSet<Manufacturer> Manufacturers { get; set; }

    public virtual DbSet<Namesproduct> Namesproducts { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<Ordersproduct> Ordersproducts { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Status> Statuses { get; set; }

    public virtual DbSet<Suplier> Supliers { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("server=10.0.8.8;database=DB10;uid=User10;pwd=KitLab@2025", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.41-mysql"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb3_general_ci")
            .HasCharSet("utf8mb3");

        modelBuilder.Entity<Address>(entity =>
        {
            entity.HasKey(e => e.IdAddress).HasName("PRIMARY");

            entity.ToTable("address");

            entity.Property(e => e.IdAddress).HasColumnName("ID_Address");
            entity.Property(e => e.AddressName)
                .HasColumnType("text")
                .HasColumnName("Address_name");
        });

        modelBuilder.Entity<Manufacturer>(entity =>
        {
            entity.HasKey(e => e.IdManufacturer).HasName("PRIMARY");

            entity.ToTable("manufacturers");

            entity.Property(e => e.IdManufacturer).HasColumnName("ID_manufacturer");
            entity.Property(e => e.Manufacturer1)
                .HasColumnType("text")
                .HasColumnName("Manufacturer");
        });

        modelBuilder.Entity<Namesproduct>(entity =>
        {
            entity.HasKey(e => e.IdNameproduct).HasName("PRIMARY");

            entity.ToTable("namesproducts");

            entity.Property(e => e.IdNameproduct).HasColumnName("ID_nameproduct");
            entity.Property(e => e.NameProduct)
                .HasColumnType("text")
                .HasColumnName("Name_Product");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.IdOrder).HasName("PRIMARY");

            entity.ToTable("orders");

            entity.HasIndex(e => e.Address, "address_idx");

            entity.HasIndex(e => e.Article, "article_idx");

            entity.HasIndex(e => e.Status, "status_idx");

            entity.HasIndex(e => e.IdUser, "user_idx");

            entity.Property(e => e.IdOrder).HasColumnName("ID_order");
            entity.Property(e => e.CodeToReceive).HasColumnName("Code_to_receive");
            entity.Property(e => e.DateDelivery)
                .HasColumnType("text")
                .HasColumnName("Date_Delivery");
            entity.Property(e => e.DateOrder)
                .HasColumnType("text")
                .HasColumnName("Date_order");
            entity.Property(e => e.IdUser).HasColumnName("ID_user");

            entity.HasOne(d => d.AddressNavigation).WithMany(p => p.Orders)
                .HasForeignKey(d => d.Address)
                .HasConstraintName("address");

            entity.HasOne(d => d.ArticleNavigation).WithMany(p => p.Orders)
                .HasForeignKey(d => d.Article)
                .HasConstraintName("article");

            entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.Orders)
                .HasForeignKey(d => d.IdUser)
                .HasConstraintName("user");

            entity.HasOne(d => d.StatusNavigation).WithMany(p => p.Orders)
                .HasForeignKey(d => d.Status)
                .HasConstraintName("status");
        });

        modelBuilder.Entity<Ordersproduct>(entity =>
        {
            entity.HasKey(e => e.IdOrderProduct).HasName("PRIMARY");

            entity.ToTable("ordersproducts");

            entity.HasIndex(e => e.Order, "order_idx");

            entity.HasIndex(e => e.Product, "prod_idx");

            entity.Property(e => e.IdOrderProduct).HasColumnName("ID_order_product");

            entity.HasOne(d => d.OrderNavigation).WithMany(p => p.Ordersproducts)
                .HasForeignKey(d => d.Order)
                .HasConstraintName("order");

            entity.HasOne(d => d.ProductNavigation).WithMany(p => p.Ordersproducts)
                .HasForeignKey(d => d.Product)
                .HasConstraintName("prod");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.IdProduct).HasName("PRIMARY");

            entity.ToTable("products");

            entity.HasIndex(e => e.Manufacturer, "manufacturer_idx");

            entity.HasIndex(e => e.NameProduct, "namepr_idx");

            entity.HasIndex(e => e.Suplier, "suplier_idx");

            entity.Property(e => e.IdProduct).HasColumnName("ID_product");
            entity.Property(e => e.Article).HasColumnType("text");
            entity.Property(e => e.Category).HasColumnType("text");
            entity.Property(e => e.Description).HasColumnType("text");
            entity.Property(e => e.NameProduct).HasColumnName("Name_Product");
            entity.Property(e => e.Photo).HasColumnType("text");
            entity.Property(e => e.ProductUnit)
                .HasColumnType("text")
                .HasColumnName("Product_unit");

            entity.HasOne(d => d.ManufacturerNavigation).WithMany(p => p.Products)
                .HasForeignKey(d => d.Manufacturer)
                .HasConstraintName("manufacturer");

            entity.HasOne(d => d.NameProductNavigation).WithMany(p => p.Products)
                .HasForeignKey(d => d.NameProduct)
                .HasConstraintName("namepr");

            entity.HasOne(d => d.SuplierNavigation).WithMany(p => p.Products)
                .HasForeignKey(d => d.Suplier)
                .HasConstraintName("suplier");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.IdRole).HasName("PRIMARY");

            entity.ToTable("roles");

            entity.Property(e => e.IdRole).HasColumnName("ID_role");
            entity.Property(e => e.RoleEmployee)
                .HasColumnType("text")
                .HasColumnName("Role_employee");
        });

        modelBuilder.Entity<Status>(entity =>
        {
            entity.HasKey(e => e.IdStatus).HasName("PRIMARY");

            entity.ToTable("status");

            entity.Property(e => e.IdStatus).HasColumnName("ID_status");
            entity.Property(e => e.Status1)
                .HasColumnType("text")
                .HasColumnName("Status");
        });

        modelBuilder.Entity<Suplier>(entity =>
        {
            entity.HasKey(e => e.IdSuplier).HasName("PRIMARY");

            entity.ToTable("supliers");

            entity.Property(e => e.IdSuplier).HasColumnName("ID_suplier");
            entity.Property(e => e.Suplier1)
                .HasColumnType("text")
                .HasColumnName("Suplier");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.IdUser).HasName("PRIMARY");

            entity.ToTable("users");

            entity.HasIndex(e => e.RoleEmployee, "role_idx");

            entity.Property(e => e.IdUser).HasColumnName("ID_User");
            entity.Property(e => e.Fio)
                .HasColumnType("text")
                .HasColumnName("FIO");
            entity.Property(e => e.Login).HasColumnType("text");
            entity.Property(e => e.Password).HasColumnType("text");
            entity.Property(e => e.RoleEmployee).HasColumnName("Role_employee");

            entity.HasOne(d => d.RoleEmployeeNavigation).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleEmployee)
                .HasConstraintName("role");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
