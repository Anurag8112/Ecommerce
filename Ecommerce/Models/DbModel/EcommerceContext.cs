using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Ecommerce.Models.DbModel
{
    public partial class EcommerceContext : DbContext
    {
        public EcommerceContext()
        {
        }

        public EcommerceContext(DbContextOptions<EcommerceContext> options)
            : base(options)
        {
        }



        public virtual DbSet<Address> Addresses { get; set; }
        public virtual DbSet<Brand> Brands { get; set; }
        public virtual DbSet<BrandCategoryMapping> BrandCategoryMappings { get; set; }
        public virtual DbSet<Cart> Carts { get; set; }
        public virtual DbSet<CartTable> CartTables { get; set; }
        public virtual DbSet<CategoryLevel1> CategoryLevel1s { get; set; }
        public virtual DbSet<CategoryLevel2> CategoryLevel2s { get; set; }
        public virtual DbSet<CategoryLevel3> CategoryLevel3s { get; set; }
        public virtual DbSet<Color> Colors { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }
        public virtual DbSet<DeliveryBoy> DeliveryBoys { get; set; }
        public virtual DbSet<DeliveryPartner> DeliveryPartners { get; set; }
        public virtual DbSet<DpHub> DpHubs { get; set; }
        public virtual DbSet<DpHubAddress> DpHubAddresses { get; set; }
        public virtual DbSet<InventryItem> InventryItems { get; set; }
        public virtual DbSet<OrderDetail> OrderDetails { get; set; }
        public virtual DbSet<OrderItem> OrderItems { get; set; }
        public virtual DbSet<PaymentDetail> PaymentDetails { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ProductDetail> ProductDetails { get; set; }
        public virtual DbSet<ProductImage> ProductImages { get; set; }
        public virtual DbSet<Size> Sizes { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserGender> UserGenders { get; set; }
        public virtual DbSet<UserProductMapping> UserProductMappings { get; set; }
        public virtual DbSet<UserRole> UserRoles { get; set; }
        public virtual DbSet<UserRoleMapping> UserRoleMappings { get; set; }
        public virtual DbSet<UserWarehouseOrderDetailsMapping> UserWarehouseOrderDetailsMappings { get; set; }
        public virtual DbSet<Warehouse> Warehouses { get; set; }
        public virtual DbSet<Wishlist> Wishlists { get; set; }
        public virtual DbSet<WishlistItem> WishlistItems { get; set; }


        public override int SaveChanges()
        {
            foreach(var entry in ChangeTracker.Entries())
            {
                var entity = entry.Entity;

                if (entry.State == EntityState.Deleted)
                {
                    entry.State = EntityState.Modified;

                    entity.GetType().GetProperty("IsActive").SetValue(entity,false);
                }
            }
            return base.SaveChanges();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=ANURAG-KATIYAR\\SQLEXPRESS;Database=Ecommerce;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Latin1_General_CI_AS");

            modelBuilder.Entity<Address>(entity =>
            {
                entity.ToTable("Address");

                entity.Property(e => e.AddressLine1)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.AddressLine2)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.City)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Country)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.State)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Addresses)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Address_User");
            });

            modelBuilder.Entity<Brand>(entity =>
            {
                entity.ToTable("Brand");

                entity.Property(e => e.BrandName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<BrandCategoryMapping>(entity =>
            {
                entity.ToTable("Brand_Category_Mapping");

                entity.Property(e => e.BrandId).HasColumnName("Brand_Id");

                entity.Property(e => e.CategoryL1Id).HasColumnName("Category_L1_Id");

                entity.HasOne(d => d.Brand)
                    .WithMany(p => p.BrandCategoryMappings)
                    .HasForeignKey(d => d.BrandId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Brand_Category_Mapping_Brand");

                entity.HasOne(d => d.CategoryL1)
                    .WithMany(p => p.BrandCategoryMappings)
                    .HasForeignKey(d => d.CategoryL1Id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Brand_Category_Mapping_Category_Level1");
            });

            modelBuilder.Entity<Cart>(entity =>
            {
                entity.ToTable("Cart");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.HasOne(d => d.CartNavigation)
                    .WithMany(p => p.Carts)
                    .HasForeignKey(d => d.CartId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Cart_Cart");

                entity.HasOne(d => d.IdNavigation)
                    .WithOne(p => p.Cart)
                    .HasForeignKey<Cart>(d => d.Id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Cart_Product_Detail");
            });

            modelBuilder.Entity<CartTable>(entity =>
            {
                entity.ToTable("CartTable");

                entity.HasIndex(e => e.UserId, "UserId_CartTable_UK")
                    .IsUnique();

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.User)
                    .WithOne(p => p.CartTable)
                    .HasForeignKey<CartTable>(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CartTable_User");
            });

            modelBuilder.Entity<CategoryLevel1>(entity =>
            {
                entity.ToTable("Category_Level1");

                entity.Property(e => e.CategoryL1)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("Category_L1");
            });

            modelBuilder.Entity<CategoryLevel2>(entity =>
            {
                entity.ToTable("Category_Level2");

                entity.Property(e => e.CategoryL1Id).HasColumnName("Category_L1_Id");

                entity.Property(e => e.CategoryL2)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("Category_L2");

                entity.HasOne(d => d.CategoryL1)
                    .WithMany(p => p.CategoryLevel2s)
                    .HasForeignKey(d => d.CategoryL1Id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Category_Level2_Category_Level1");
            });

            modelBuilder.Entity<CategoryLevel3>(entity =>
            {
                entity.ToTable("Category_Level3");

                entity.Property(e => e.CategoryL2Id).HasColumnName("Category_L2_Id");

                entity.Property(e => e.CategoryL3)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("Category_L3");

                entity.HasOne(d => d.CategoryL2)
                    .WithMany(p => p.CategoryLevel3s)
                    .HasForeignKey(d => d.CategoryL2Id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Category_Level3_Category_Level21");
            });

            modelBuilder.Entity<Color>(entity =>
            {
                entity.ToTable("Color");

                entity.Property(e => e.Color1)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("Color");
            });

            modelBuilder.Entity<Comment>(entity =>
            {
                entity.Property(e => e.Comment1)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("Comment");

                entity.Property(e => e.ProdId).HasColumnName("Prod_Id");

                entity.Property(e => e.UserId).HasColumnName("User_Id");

                entity.HasOne(d => d.Prod)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.ProdId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Comments_Products");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Comments_User");
            });

            modelBuilder.Entity<DeliveryBoy>(entity =>
            {
                entity.ToTable("DeliveryBoy");

                entity.HasIndex(e => e.AssignedHubId, "Hub_UK")
                    .IsUnique();

                entity.HasIndex(e => e.Id, "UniqueKey")
                    .IsUnique();

                entity.Property(e => e.AssignedHubId).HasColumnName("Assigned_Hub_Id");

                entity.Property(e => e.UserRoleMappingId).HasColumnName("User_Role_Mapping_Id");

                entity.HasOne(d => d.AssignedHub)
                    .WithOne(p => p.DeliveryBoy)
                    .HasForeignKey<DeliveryBoy>(d => d.AssignedHubId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DeliveryBoy_DP_Hubs");

                entity.HasOne(d => d.UserRoleMapping)
                    .WithMany(p => p.DeliveryBoys)
                    .HasForeignKey(d => d.UserRoleMappingId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DeliveryBoy_User_Role_Mapping");
            });

            modelBuilder.Entity<DeliveryPartner>(entity =>
            {
                entity.Property(e => e.DeliveryPartnerName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Delivery_Partner_Name");

                entity.Property(e => e.HubManagerId).HasColumnName("Hub_Manager_Id");

                entity.HasOne(d => d.HubManager)
                    .WithMany(p => p.DeliveryPartners)
                    .HasForeignKey(d => d.HubManagerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DeliveryPartners_User_Warehouse_OrderDetails_Mapping");
            });

            modelBuilder.Entity<DpHub>(entity =>
            {
                entity.ToTable("DP_Hubs");

                entity.Property(e => e.DpId).HasColumnName("DP_Id");

                entity.Property(e => e.HubName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Hub_Name");

                entity.HasOne(d => d.Dp)
                    .WithMany(p => p.DpHubs)
                    .HasForeignKey(d => d.DpId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DP_Hubs_DeliveryPartners");
            });

            modelBuilder.Entity<DpHubAddress>(entity =>
            {
                entity.ToTable("DP_Hub_Address");

                entity.HasIndex(e => e.DpHubId, "DP_Hub_Address_UK")
                    .IsUnique();

                entity.Property(e => e.AddressLine1)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Address_Line1");

                entity.Property(e => e.AddressLine2)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Address_Line2");

                entity.Property(e => e.City)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Country)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.DpHubId).HasColumnName("DP_Hub_Id");

                entity.Property(e => e.State)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.HasOne(d => d.DpHub)
                    .WithOne(p => p.DpHubAddress)
                    .HasForeignKey<DpHubAddress>(d => d.DpHubId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DP_Hub_Address_DP_Hubs");
            });

            modelBuilder.Entity<InventryItem>(entity =>
            {
                entity.ToTable("Inventry_Item");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.ProductCount).HasColumnName("Product_Count");

                entity.Property(e => e.ProductDetailId).HasColumnName("Product_Detail_Id");

                entity.Property(e => e.WarehouseId).HasColumnName("Warehouse_Id");

                entity.HasOne(d => d.IdNavigation)
                    .WithOne(p => p.InventryItem)
                    .HasForeignKey<InventryItem>(d => d.Id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Inventry_Item_Product_Detail");

                entity.HasOne(d => d.Warehouse)
                    .WithMany(p => p.InventryItems)
                    .HasForeignKey(d => d.WarehouseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Inventry_Item_Warehouse");
            });

            modelBuilder.Entity<OrderDetail>(entity =>
            {
                entity.HasIndex(e => e.PaymentId, "UK")
                    .IsUnique();

                entity.Property(e => e.Time)
                    .IsRequired()
                    .IsRowVersion()
                    .IsConcurrencyToken();

                entity.HasOne(d => d.Address)
                    .WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.AddressId)
                    .HasConstraintName("FK_OrderDetails_Address");

                entity.HasOne(d => d.Payment)
                    .WithOne(p => p.OrderDetail)
                    .HasForeignKey<OrderDetail>(d => d.PaymentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OrderDetails_PaymentDetails");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OrderDetails_User");
            });

            modelBuilder.Entity<OrderItem>(entity =>
            {
                entity.ToTable("Order_Items");

                entity.Property(e => e.Date).HasColumnType("date");

                entity.Property(e => e.Time)
                    .IsRequired()
                    .IsRowVersion()
                    .IsConcurrencyToken();

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderItems)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Order_Items_OrderDetails");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.OrderItems)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Order_Items_Products");
            });

            modelBuilder.Entity<PaymentDetail>(entity =>
            {
                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Time)
                    .IsRequired()
                    .IsRowVersion()
                    .IsConcurrencyToken();
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(e => e.ProdId);

                entity.Property(e => e.ProdId).HasColumnName("Prod_Id");

                entity.Property(e => e.BrandId).HasColumnName("Brand_Id");

                entity.Property(e => e.ProdDescription)
                    .IsRequired()
                    .HasMaxLength(500)
                    .HasColumnName("Prod_Description");

                entity.Property(e => e.ProdName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Prod_Name");

                entity.HasOne(d => d.Brand)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.BrandId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Products_Brand");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Products_Category_Level1");
            });

            modelBuilder.Entity<ProductDetail>(entity =>
            {
                entity.ToTable("Product_Detail");

                entity.Property(e => e.ProdId).HasColumnName("Prod_Id");

                entity.HasOne(d => d.Color)
                    .WithMany(p => p.ProductDetails)
                    .HasForeignKey(d => d.ColorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Product_Detail_Color");

                entity.HasOne(d => d.Prod)
                    .WithMany(p => p.ProductDetails)
                    .HasForeignKey(d => d.ProdId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Product_Detail_Products");

                entity.HasOne(d => d.Size)
                    .WithMany(p => p.ProductDetails)
                    .HasForeignKey(d => d.SizeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Product_Detail_Size");
            });

            modelBuilder.Entity<ProductImage>(entity =>
            {
                entity.HasKey(e => e.ImgId);

                entity.ToTable("Product_Image");

                entity.Property(e => e.ImgId).HasColumnName("Img_Id");

                entity.Property(e => e.Image)
                    .IsRequired()
                    .HasColumnType("image");

                entity.Property(e => e.ProdId).HasColumnName("Prod_Id");

                entity.HasOne(d => d.Prod)
                    .WithMany(p => p.ProductImages)
                    .HasForeignKey(d => d.ProdId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Product_Image_Products");
            });

            modelBuilder.Entity<Size>(entity =>
            {
                entity.ToTable("Size");

                entity.Property(e => e.Size1)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("Size");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.GenderId).HasColumnName("Gender_Id");

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(int.MaxValue)
                    .IsUnicode(false);

                entity.Property(e => e.Phone)
                    .IsRequired()
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.Property(e => e.IsVerified)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Gender)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.GenderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_User_UserGender");
            });

            modelBuilder.Entity<UserGender>(entity =>
            {
                entity.ToTable("UserGender");

                entity.Property(e => e.Gender)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<UserProductMapping>(entity =>
            {
                entity.ToTable("User_Product_mapping");

                entity.Property(e => e.ProdId).HasColumnName("Prod_Id");

                entity.Property(e => e.UserId).HasColumnName("User_ID");

                entity.HasOne(d => d.Prod)
                    .WithMany(p => p.UserProductMappings)
                    .HasForeignKey(d => d.ProdId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_User_Product_mapping_Products");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserProductMappings)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_User_Product_mapping_User");
            });

            modelBuilder.Entity<UserRole>(entity =>
            {
                entity.ToTable("UserRole");

                entity.Property(e => e.Role)
                    .IsRequired()
                    .HasMaxLength(25)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<UserRoleMapping>(entity =>
            {
                entity.ToTable("User_Role_Mapping");

                entity.Property(e => e.RoleId).HasColumnName("Role_Id");

                entity.Property(e => e.UserId).HasColumnName("User_Id");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.UserRoleMappings)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_User_Role_Mapping_UserRole");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserRoleMappings)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_User_Role_Mapping_User");
            });

            modelBuilder.Entity<UserWarehouseOrderDetailsMapping>(entity =>
            {
                entity.ToTable("User_Warehouse_OrderDetails_Mapping");

                entity.Property(e => e.OrderDetailId).HasColumnName("Order_Detail_Id");

                entity.Property(e => e.UserRoleMappingId).HasColumnName("User_Role_Mapping_Id");

                entity.Property(e => e.WarehouseId).HasColumnName("Warehouse_Id");

                entity.HasOne(d => d.OrderDetail)
                    .WithMany(p => p.UserWarehouseOrderDetailsMappings)
                    .HasForeignKey(d => d.OrderDetailId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_User_Warehouse_OrderDetails_Mapping_OrderDetails");

                entity.HasOne(d => d.UserRoleMapping)
                    .WithMany(p => p.UserWarehouseOrderDetailsMappings)
                    .HasForeignKey(d => d.UserRoleMappingId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_User_Warehouse_OrderDetails_Mapping_User");

                entity.HasOne(d => d.Warehouse)
                    .WithMany(p => p.UserWarehouseOrderDetailsMappings)
                    .HasForeignKey(d => d.WarehouseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_User_Warehouse_OrderDetails_Mapping_Warehouse");
            });

            modelBuilder.Entity<Warehouse>(entity =>
            {
                entity.ToTable("Warehouse");

                entity.Property(e => e.WarehouseName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Warehouse_Name");
            });

            modelBuilder.Entity<Wishlist>(entity =>
            {
                entity.ToTable("Wishlist");

                entity.HasIndex(e => e.UserId, "Wishlist_UserId_UK")
                    .IsUnique();

                entity.HasOne(d => d.User)
                    .WithOne(p => p.Wishlist)
                    .HasForeignKey<Wishlist>(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Wishlist_User");
            });

            modelBuilder.Entity<WishlistItem>(entity =>
            {
                entity.ToTable("Wishlist_Item");

                entity.Property(e => e.ProdDetailId).HasColumnName("Prod_Detail_Id");

                entity.Property(e => e.WishlistId).HasColumnName("Wishlist_Id");

                entity.HasOne(d => d.ProdDetail)
                    .WithMany(p => p.WishlistItems)
                    .HasForeignKey(d => d.ProdDetailId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Wishlist_Item_Product_Detail");

                entity.HasOne(d => d.Wishlist)
                    .WithMany(p => p.WishlistItems)
                    .HasForeignKey(d => d.WishlistId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Wishlist_Item_Wishlist");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
