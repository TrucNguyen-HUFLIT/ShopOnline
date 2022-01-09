using Microsoft.EntityFrameworkCore;
using ShopOnline.Core.Entities;

namespace ShopOnline.Core
{
    public class MyDbContext : DbContext
    {
        #region Constructor

        public MyDbContext()
        {

        }

        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
        {

        }

        #endregion

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
                optionsBuilder.UseSqlServer(@"Server=HUYNGUYENGIA;Initial Catalog=ShopOnline_Shoes1;User ID=sa;Password=sa123;Trusted_Connection=False;");

        }

        #region DbSet

        public DbSet<ReviewDetailEntity> ReviewDetails { get; set; }
        public DbSet<StaffEntity> Staffs { get; set; }
        public DbSet<CustomerEntity> Customers { get; set; }
        public DbSet<OrderEntity> Orders { get; set; }
        public DbSet<ShipperEntity> Shippers { get; set; }
        public DbSet<OrderDetailEntity> OrderDetails { get; set; }
        public DbSet<ProductDetailEntity> ProductDetails { get; set; }
        public DbSet<ProductTypeEntity> ProductTypes { get; set; }

        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<StaffEntity>(entity =>
            {
                entity.ToTable("Staff");
                entity.HasKey(x => x.Id);

                entity.Property(x => x.FullName).IsRequired();
                entity.Property(x => x.Email).IsRequired();
                entity.Property(x => x.Password).IsRequired();
                entity.Property(x => x.PhoneNumber).IsRequired();
                entity.Property(x => x.TypeAcc).IsRequired();
                entity.Property(x => x.Salary).IsRequired();
            });

            modelBuilder.Entity<ReviewDetailEntity>(entity =>
            {
                entity.ToTable("ReviewDetail");
                entity.HasKey(x => x.Id);

                entity.HasOne(x => x.Customer)
                    .WithMany(x => x.ReviewDetails)
                    .HasForeignKey(x => x.IdCustomer);

                entity.HasOne(x => x.ProductDetail)
                    .WithMany(x => x.ReviewDetails)
                    .HasForeignKey(x => x.IdProductDetail);

                entity.Property(x => x.Content).IsRequired();
                entity.Property(x => x.ReviewTime).IsRequired();
                entity.Property(x => x.ReviewStatus).IsRequired();
            });

            modelBuilder.Entity<CustomerEntity>(entity =>
            {
                entity.ToTable("Customer");
                entity.HasKey(x => x.Id);

                entity.Property(x => x.FullName).IsRequired();
                entity.Property(x => x.Email).IsRequired();
                entity.Property(x => x.Password).IsRequired();
                entity.Property(x => x.PhoneNumber).IsRequired();
                entity.Property(x => x.TypeAcc).IsRequired();
            });

            modelBuilder.Entity<ShipperEntity>(entity =>
            {
                entity.ToTable("Shipper");
                entity.HasKey(x => x.Id);

                entity.Property(x => x.FullName).IsRequired();
                entity.Property(x => x.Email).IsRequired();
                entity.Property(x => x.Password).IsRequired();
                entity.Property(x => x.PhoneNumber).IsRequired();
                entity.Property(x => x.TypeAcc).IsRequired();
                entity.Property(x => x.Salary).IsRequired();
            });

            modelBuilder.Entity<OrderEntity>(entity =>
            {
                entity.ToTable("Order");
                entity.HasKey(x => x.Id);

                entity.HasOne(x => x.Customer)
                    .WithMany(x => x.Orders)
                    .HasForeignKey(x => x.IdCustomer);

                entity.HasOne(x => x.Shipper)
                    .WithMany(x => x.Orders)
                    .HasForeignKey(x => x.IdShipper);

                entity.Property(x => x.IsPaid).IsRequired();
                entity.Property(x => x.OrderDay).IsRequired();
                entity.Property(x => x.StatusOrder).IsRequired();
                entity.Property(x => x.Payment).IsRequired();
            });

            modelBuilder.Entity<OrderDetailEntity>(entity =>
            {
                entity.ToTable("OrderDetail");
                entity.HasKey(x => new { x.IdOrder, x.IdProductDetail });

                entity.HasOne(x => x.ProductDetail)
                    .WithMany(x => x.OrderDetails)
                    .HasForeignKey(x => x.IdProductDetail);

                entity.HasOne(x => x.Order)
                    .WithMany(x => x.OrderDetails)
                    .HasForeignKey(x => x.IdOrder);

                entity.Property(x => x.TotalPrice).IsRequired();
                entity.Property(x => x.TotalBasePrice).IsRequired();
                entity.Property(x => x.QuantityPurchased).IsRequired();
            });

            modelBuilder.Entity<ProductDetailEntity>(entity =>
            {
                entity.ToTable("ProductDetail");
                entity.HasKey(x => x.Id);

                entity.HasOne(x => x.ProductType)
                    .WithMany(x => x.ProductDetails)
                    .HasForeignKey(x => x.IdProductType);

                entity.Property(x => x.Name).IsRequired();
                entity.Property(x => x.Price).IsRequired();
                entity.Property(x => x.BasePrice).IsRequired();
                entity.Property(x => x.Status).IsRequired();
                entity.Property(x => x.Quantity).IsRequired();
            });

            modelBuilder.Entity<ProductTypeEntity>(entity =>
            {
                entity.ToTable("ProductType");
                entity.HasKey(x => x.Id);

                entity.Property(x => x.Name).IsRequired();
            });
        }
    }
}
