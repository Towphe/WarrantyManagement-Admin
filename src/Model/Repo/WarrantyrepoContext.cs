using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace src.Model.Repo;

public partial class WarrantyrepoContext : DbContext
{
    public WarrantyrepoContext()
    {
    }

    public WarrantyrepoContext(DbContextOptions<WarrantyrepoContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Distributor> Distributors { get; set; }

    public virtual DbSet<Entry> Entries { get; set; }

    public virtual DbSet<Merchant> Merchants { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<User> Users { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("category_pkey");

            entity.ToTable("category");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.DateAdded)
                .HasDefaultValueSql("CURRENT_DATE")
                .HasColumnName("date_added");
            entity.Property(e => e.Subtype)
                .HasMaxLength(70)
                .HasColumnName("subtype");
            entity.Property(e => e.Type)
                .HasMaxLength(70)
                .HasColumnName("type");
            entity.Property(e => e.Uniq)
                .HasMaxLength(100)
                .HasColumnName("uniq");
        });

        modelBuilder.Entity<Distributor>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("distributor_pkey");

            entity.ToTable("distributor");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.DateAdded)
                .HasDefaultValueSql("CURRENT_DATE")
                .HasColumnName("date_added");
            entity.Property(e => e.Deleted)
                .HasDefaultValueSql("false")
                .HasColumnName("deleted");
            entity.Property(e => e.Name)
                .HasMaxLength(70)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Entry>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("entryr_pkey");

            entity.ToTable("entry");

            entity.Property(e => e.Id)
                .HasMaxLength(11)
                .HasColumnName("id");
            entity.Property(e => e.ContactNumber)
                .HasMaxLength(13)
                .HasColumnName("contact_number");
            entity.Property(e => e.DateAdded)
                .HasDefaultValueSql("CURRENT_DATE")
                .HasColumnName("date_added");
            entity.Property(e => e.DatePurchased).HasColumnName("date_purchased");
            entity.Property(e => e.Deleted)
                .HasDefaultValueSql("false")
                .HasColumnName("deleted");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Email)
                .HasMaxLength(254)
                .HasColumnName("email");
            entity.Property(e => e.FirstName)
                .HasMaxLength(70)
                .HasColumnName("first_name");
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .HasColumnName("last_name");
            entity.Property(e => e.MediaDir)
                .HasMaxLength(255)
                .HasColumnName("media_dir");
            entity.Property(e => e.ProductId).HasColumnName("product_id");
            entity.Property(e => e.ReferenceNum)
                .HasMaxLength(50)
                .HasColumnName("reference_num");
            entity.Property(e => e.SalesOrderNum)
                .HasMaxLength(30)
                .HasColumnName("sales_order_num");
            entity.Property(e => e.Status)
                .HasMaxLength(25)
                .HasColumnName("status");
            entity.Property(e => e.Store)
                .HasMaxLength(50)
                .HasColumnName("store");
            entity.Property(e => e.Variation)
                .HasMaxLength(255)
                .HasColumnName("variation");

            entity.HasOne(d => d.Product).WithMany(p => p.Entries)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_product");
        });

        modelBuilder.Entity<Merchant>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("merchant_pkey");

            entity.ToTable("merchant");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.Company)
                .HasMaxLength(70)
                .HasColumnName("company");
            entity.Property(e => e.DateAdded)
                .HasDefaultValueSql("CURRENT_DATE")
                .HasColumnName("date_added");
            entity.Property(e => e.Deleted)
                .HasDefaultValueSql("false")
                .HasColumnName("deleted");
            entity.Property(e => e.Name)
                .HasMaxLength(70)
                .HasColumnName("name");
            entity.Property(e => e.Platform)
                .HasMaxLength(70)
                .HasColumnName("platform");
            entity.Property(e => e.Uniq)
                .HasMaxLength(100)
                .HasColumnName("uniq");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("product_pkey");

            entity.ToTable("product");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.CategoryId).HasColumnName("category_id");
            entity.Property(e => e.DateAdded)
                .HasDefaultValueSql("CURRENT_DATE")
                .HasColumnName("date_added");
            entity.Property(e => e.Deleted).HasColumnName("deleted");
            entity.Property(e => e.DistributorId).HasColumnName("distributor_id");
            entity.Property(e => e.MerchantId).HasColumnName("merchant_id");
            entity.Property(e => e.Name)
                .HasMaxLength(70)
                .HasColumnName("name");
            entity.Property(e => e.Variants)
                .HasColumnType("json")
                .HasColumnName("variants");

            entity.HasOne(d => d.Category).WithMany(p => p.Products)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_category");

            entity.HasOne(d => d.Distributor).WithMany(p => p.Products)
                .HasForeignKey(d => d.DistributorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_distributor");

            entity.HasOne(d => d.Merchant).WithMany(p => p.Products)
                .HasForeignKey(d => d.MerchantId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_merchant");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("users_pkey");

            entity.ToTable("users");

            entity.Property(e => e.Id)
                .HasMaxLength(11)
                .HasColumnName("id");
            entity.Property(e => e.Deleted)
                .HasDefaultValueSql("false")
                .HasColumnName("deleted");
            entity.Property(e => e.Email)
                .HasMaxLength(254)
                .HasColumnName("email");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .HasColumnName("password");
            entity.Property(e => e.UserToken)
                .HasMaxLength(255)
                .HasDefaultValueSql("'N/A'::character varying")
                .HasColumnName("user_token");
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .HasColumnName("username");
            entity.Property(e => e.Vcode)
                .HasMaxLength(6)
                .HasColumnName("vcode");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
