using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CappyPop_Full_HTML.Models.Tables
{
    public partial class bobateashopContext : DbContext
    {
        public bobateashopContext()
        {
        }

        public bobateashopContext(DbContextOptions<bobateashopContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Account> Accounts { get; set; } = null!;
        public virtual DbSet<Adminseller> Adminsellers { get; set; } = null!;
        public virtual DbSet<Blog> Blogs { get; set; } = null!;
        public virtual DbSet<Bobatea> Bobateas { get; set; } = null!;
        public virtual DbSet<Customer> Customers { get; set; } = null!;
        public virtual DbSet<Ice> Ices { get; set; } = null!;
        public virtual DbSet<IceBobatea> IceBobateas { get; set; } = null!;
        public virtual DbSet<ImageUrl> ImageUrls { get; set; } = null!;
        public virtual DbSet<Order> Orders { get; set; } = null!;
        public virtual DbSet<OrderDetail> OrderDetails { get; set; } = null!;
        public virtual DbSet<Payment> Payments { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<Size> Sizes { get; set; } = null!;
        public virtual DbSet<SizeBobatea> SizeBobateas { get; set; } = null!;
        public virtual DbSet<Sugar> Sugars { get; set; } = null!;
        public virtual DbSet<SugarBobatea> SugarBobateas { get; set; } = null!;
        public virtual DbSet<Topping> Toppings { get; set; } = null!;
        public virtual DbSet<ToppingBobatea> ToppingBobateas { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseMySql("server=localhost;database=bobateashop;user=root;password=thuthu121;allow user variables=True", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.40-mysql"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("utf8mb4_0900_ai_ci")
                .HasCharSet("utf8mb4");

            modelBuilder.Entity<Account>(entity =>
            {
                entity.ToTable("account");

                entity.HasIndex(e => e.RoleId, "RoleID");

                entity.HasIndex(e => e.Username, "Username")
                    .IsUnique();

                entity.Property(e => e.AccountId).HasColumnName("AccountID");

                entity.Property(e => e.Password).HasMaxLength(255);

                entity.Property(e => e.RoleId).HasColumnName("RoleID");

                entity.Property(e => e.Username).HasMaxLength(50);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Accounts)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("account_ibfk_1");
            });

            modelBuilder.Entity<Adminseller>(entity =>
            {
                entity.ToTable("adminseller");

                entity.HasIndex(e => e.AccountId, "AccountID");

                entity.HasIndex(e => e.Email, "Email")
                    .IsUnique();

                entity.Property(e => e.AdminSellerId).HasColumnName("AdminSellerID");

                entity.Property(e => e.AccountId).HasColumnName("AccountID");

                entity.Property(e => e.Email).HasMaxLength(100);

                entity.Property(e => e.Name).HasMaxLength(100);

                entity.Property(e => e.Phone).HasMaxLength(20);

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.Adminsellers)
                    .HasForeignKey(d => d.AccountId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("adminseller_ibfk_1");
            });

            modelBuilder.Entity<Blog>(entity =>
            {
                entity.ToTable("blog");

                entity.HasIndex(e => e.AuthorId, "AuthorID");

                entity.Property(e => e.BlogId).HasColumnName("BlogID");

                entity.Property(e => e.AuthorId).HasColumnName("AuthorID");

                entity.Property(e => e.Content).HasColumnType("text");

                entity.Property(e => e.PublishedDate).HasColumnType("datetime");

                entity.Property(e => e.Title).HasMaxLength(200);

                entity.HasOne(d => d.Author)
                    .WithMany(p => p.Blogs)
                    .HasForeignKey(d => d.AuthorId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("blog_ibfk_1");
            });

            modelBuilder.Entity<Bobatea>(entity =>
            {
                entity.ToTable("bobatea");

                entity.HasIndex(e => e.AdminSeller, "bobatea_ibfk_1_idx");

                entity.Property(e => e.BobaTeaId).HasColumnName("BobaTeaID");

                entity.Property(e => e.AdminSeller).HasColumnName("Admin_Seller");

                entity.Property(e => e.Description).HasColumnType("text");

                entity.Property(e => e.Name).HasMaxLength(100);

                entity.Property(e => e.Price).HasPrecision(10, 2);

                entity.HasOne(d => d.AdminSellerNavigation)
                    .WithMany(p => p.Bobateas)
                    .HasForeignKey(d => d.AdminSeller)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("bobatea_ibfk_1");
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.ToTable("customer");

                entity.HasIndex(e => e.AccountId, "AccountID");

                entity.HasIndex(e => e.Email, "Email")
                    .IsUnique();

                entity.Property(e => e.CustomerId).HasColumnName("CustomerID");

                entity.Property(e => e.AccountId).HasColumnName("AccountID");

                entity.Property(e => e.Address).HasMaxLength(255);

                entity.Property(e => e.Email).HasMaxLength(100);

                entity.Property(e => e.Name).HasMaxLength(100);

                entity.Property(e => e.Phone).HasMaxLength(20);

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.Customers)
                    .HasForeignKey(d => d.AccountId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("customer_ibfk_1");
            });

            modelBuilder.Entity<Ice>(entity =>
            {
                entity.ToTable("ice");

                entity.Property(e => e.IceId).HasColumnName("IceID");

                entity.Property(e => e.IceLevel).HasMaxLength(50);
            });

            modelBuilder.Entity<IceBobatea>(entity =>
            {
                entity.ToTable("ice_bobatea");

                entity.HasIndex(e => e.BobaTeaId, "BobaTeaID");

                entity.HasIndex(e => e.IceId, "IceID");

                entity.Property(e => e.BobaTeaId).HasColumnName("BobaTeaID");

                entity.Property(e => e.IceId).HasColumnName("IceID");

                entity.HasOne(d => d.BobaTea)
                    .WithMany(p => p.IceBobateas)
                    .HasForeignKey(d => d.BobaTeaId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("ice_bobatea_ibfk_1");

                entity.HasOne(d => d.Ice)
                    .WithMany(p => p.IceBobateas)
                    .HasForeignKey(d => d.IceId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("ice_bobatea_ibfk_2");
            });

            modelBuilder.Entity<ImageUrl>(entity =>
            {
                entity.HasKey(e => e.ImageId)
                    .HasName("PRIMARY");

                entity.ToTable("image_url");

                entity.HasIndex(e => e.BobaTeaId, "BobaTeaID");

                entity.Property(e => e.ImageId).HasColumnName("ImageID");

                entity.Property(e => e.BobaTeaId).HasColumnName("BobaTeaID");

                entity.Property(e => e.IsPrimary).HasDefaultValueSql("'0'");

                entity.Property(e => e.Url).HasColumnType("text");

                entity.HasOne(d => d.BobaTea)
                    .WithMany(p => p.ImageUrls)
                    .HasForeignKey(d => d.BobaTeaId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("image_url_ibfk_1");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("orders");

                entity.HasIndex(e => e.AdminSellerId, "AdminSellerID");

                entity.HasIndex(e => e.CustomerId, "CustomerID");

                entity.HasIndex(e => e.PaymentId, "payment_id");

                entity.Property(e => e.OrderId).HasColumnName("OrderID");

                entity.Property(e => e.AdminSellerId).HasColumnName("AdminSellerID");

                entity.Property(e => e.CustomerId).HasColumnName("CustomerID");

                entity.Property(e => e.OrderDate).HasColumnType("datetime");

                entity.Property(e => e.PaidDate).HasColumnType("datetime");

                entity.Property(e => e.PaymentId).HasColumnName("payment_id");

                entity.Property(e => e.Status).HasMaxLength(50);

                entity.Property(e => e.TotalAmount).HasPrecision(10, 2);

                entity.Property(e => e.VnpayRef)
                    .HasMaxLength(50)
                    .HasColumnName("vnpay_ref");

                entity.HasOne(d => d.AdminSeller)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.AdminSellerId)
                    .HasConstraintName("orders_ibfk_3");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("orders_ibfk_2");

                entity.HasOne(d => d.Payment)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.PaymentId)
                    .HasConstraintName("orders_ibfk_1");
            });

            modelBuilder.Entity<OrderDetail>(entity =>
            {
                entity.ToTable("order_details");

                entity.HasIndex(e => e.BobaTeaId, "BobaTeaID");

                entity.HasIndex(e => e.OrderId, "OrderID");

                entity.Property(e => e.OrderDetailId).HasColumnName("OrderDetailID");

                entity.Property(e => e.BobaTeaId).HasColumnName("BobaTeaID");

                entity.Property(e => e.OrderId).HasColumnName("OrderID");

                entity.HasOne(d => d.BobaTea)
                    .WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.BobaTeaId)
                    .HasConstraintName("order_details_ibfk_2");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.OrderId)
                    .HasConstraintName("order_details_ibfk_1");
            });

            modelBuilder.Entity<Payment>(entity =>
            {
                entity.ToTable("payment");

                entity.Property(e => e.PaymentId).HasColumnName("PaymentID");

                entity.Property(e => e.Method).HasMaxLength(45);
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("role");

                entity.Property(e => e.RoleId).HasColumnName("RoleID");

                entity.Property(e => e.RoleName).HasMaxLength(50);
            });

            modelBuilder.Entity<Size>(entity =>
            {
                entity.ToTable("size");

                entity.Property(e => e.SizeId).HasColumnName("SizeID");

                entity.Property(e => e.SizeName).HasMaxLength(50);
            });

            modelBuilder.Entity<SizeBobatea>(entity =>
            {
                entity.ToTable("size_bobatea");

                entity.HasIndex(e => e.BobaTeaId, "BobaTeaID");

                entity.HasIndex(e => e.SizeId, "SizeID");

                entity.Property(e => e.BobaTeaId).HasColumnName("BobaTeaID");

                entity.Property(e => e.SizeId).HasColumnName("SizeID");

                entity.HasOne(d => d.BobaTea)
                    .WithMany(p => p.SizeBobateas)
                    .HasForeignKey(d => d.BobaTeaId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("size_bobatea_ibfk_1");

                entity.HasOne(d => d.Size)
                    .WithMany(p => p.SizeBobateas)
                    .HasForeignKey(d => d.SizeId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("size_bobatea_ibfk_2");
            });

            modelBuilder.Entity<Sugar>(entity =>
            {
                entity.ToTable("sugar");

                entity.Property(e => e.SugarId).HasColumnName("SugarID");

                entity.Property(e => e.SugarLevel).HasMaxLength(50);
            });

            modelBuilder.Entity<SugarBobatea>(entity =>
            {
                entity.ToTable("sugar_bobatea");

                entity.HasIndex(e => e.BobaTeaId, "BobaTeaID");

                entity.HasIndex(e => e.SugarId, "SugarID");

                entity.Property(e => e.BobaTeaId).HasColumnName("BobaTeaID");

                entity.Property(e => e.SugarId).HasColumnName("SugarID");

                entity.HasOne(d => d.BobaTea)
                    .WithMany(p => p.SugarBobateas)
                    .HasForeignKey(d => d.BobaTeaId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("sugar_bobatea_ibfk_1");

                entity.HasOne(d => d.Sugar)
                    .WithMany(p => p.SugarBobateas)
                    .HasForeignKey(d => d.SugarId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("sugar_bobatea_ibfk_2");
            });

            modelBuilder.Entity<Topping>(entity =>
            {
                entity.ToTable("topping");

                entity.Property(e => e.ToppingId).HasColumnName("ToppingID");

                entity.Property(e => e.Name).HasMaxLength(100);

                entity.Property(e => e.Price).HasPrecision(10, 2);
            });

            modelBuilder.Entity<ToppingBobatea>(entity =>
            {
                entity.ToTable("topping_bobatea");

                entity.HasIndex(e => e.BobaTeaId, "BobaTeaID");

                entity.HasIndex(e => e.ToppingId, "ToppingID");

                entity.Property(e => e.BobaTeaId).HasColumnName("BobaTeaID");

                entity.Property(e => e.ToppingId).HasColumnName("ToppingID");

                entity.HasOne(d => d.BobaTea)
                    .WithMany(p => p.ToppingBobateas)
                    .HasForeignKey(d => d.BobaTeaId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("topping_bobatea_ibfk_1");

                entity.HasOne(d => d.Topping)
                    .WithMany(p => p.ToppingBobateas)
                    .HasForeignKey(d => d.ToppingId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("topping_bobatea_ibfk_2");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
