using System;
using Microsoft.EntityFrameworkCore;
using Kalabean.Domain.Entities;
using Kalabean.Infrastructure.Extensions.SchemaDefinitions;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Kalabean.Infrastructure.AppSettingConfigs.Images;

namespace Kalabean.Infrastructure
{
    public class AppDbContext : IdentityDbContext<User,
                                                  Role,
                                                  long,
                                                  UserClaim,
                                                  UserRole,
                                                  UserLogin,
                                                  RoleClaim,
                                                  UserToken>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options,
                            IHttpContextAccessor httpContext,
                            IOptions<AppSettingConfigs.Files> imagePath) : base(options)
        {

            Helpers.JWTTokenManager.HttpContext = httpContext.HttpContext;
            Helpers.ReturnFilePath.BaseUrl = imagePath.Value.BaseUrl;
        }
        //public DbSet<Category> Categories{ get; set; }
        //public DbSet<Article> Articles { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ShoppingCenterType> ShoppingCenterTypes { get; set; }
        public DbSet<ShoppingCenter> ShoppingCenters { get; set; }
        public DbSet<Floor> Floors { get; set; }
        public DbSet<Store> Stores { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<OrderHeader> OrderHeaders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Requirement> Requirements { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }
        public DbSet<RequirementUserSeen> RequirementUserSeens { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<ProductComment> ProductComments { get; set; }
        public DbSet<Advertise> Advertises { get; set; }
        public DbSet<PossibilitiesShopCenter> PossibilitiesShopCenters { get; set; }
        public DbSet<Conversation> Conversations { get; set; }
        public DbSet<ConversationDetail> ConversationDetails { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CategoryEntitySchemaDefinition());
            modelBuilder.ApplyConfiguration(new CityEntitySchemaDefinition());
            modelBuilder.ApplyConfiguration(new ShoppingCenterTypeEntitySchemaDefinition());
            modelBuilder.ApplyConfiguration(new ShoppingCenterEntitySchemaDefinition());
            modelBuilder.ApplyConfiguration(new FloorEntitySchemaDefinition());
            modelBuilder.ApplyConfiguration(new StoreEntitySchemaDefinition());
            modelBuilder.ApplyConfiguration(new ProductEntitySchemaDefinition());
            modelBuilder.ApplyConfiguration(new ProductImageEntitySchemaDefinition());
            modelBuilder.ApplyConfiguration(new ProductImageEntitySchemaDefinition());
            modelBuilder.ApplyConfiguration(new OrderHeaderEntitySchemaDefinition());
            modelBuilder.ApplyConfiguration(new OrderDetailEntitySchemaDefinition());
            modelBuilder.ApplyConfiguration(new RequirementEntitySchemaDefinition());
            modelBuilder.ApplyConfiguration(new UserEntitySchemaDefinition());
            modelBuilder.ApplyConfiguration(new RoleEntitySchemaDefinition());
            modelBuilder.ApplyConfiguration(new RolePermissionEntitySchemaDefinition());
            modelBuilder.ApplyConfiguration(new ArticleEntitySchemaDefinition());
            modelBuilder.ApplyConfiguration(new ProductCommentEntitySchemaDefinition());
            modelBuilder.ApplyConfiguration(new AdvertiseEntitySchemaDefinition());
            modelBuilder.ApplyConfiguration(new PossibilitiesShopCenterEntitySchemaDefinition());
            modelBuilder.ApplyConfiguration(new ConversationEntitySchemaDefinition());
            modelBuilder.ApplyConfiguration(new ConversationDetailEntitySchemaDefinition());
            modelBuilder.ApplyConfiguration(new TicketEntitySchemaDefinition());
            modelBuilder.ApplyConfiguration(new TicketDetailEntitySchemaDefinition());

            base.OnModelCreating(modelBuilder);

            #region Seed
            #region  User
            modelBuilder.Entity<User>().HasData(new User()
            {
                Id = 1,
                UserName = "09150000000",
                NormalizedUserName = "09150000000",
                PasswordHash = "AQAAAAEAACcQAAAAENSkzWsQZKhTh+7aBZLEAWRHo8O3XC0qp1d1RFJEdvxKt3rGy+8Agyt38iYrVR5Zyw==",
                SecurityStamp = "JF5Z6SA4QDPB246AF2WKXR5B5QAMMN7O",
                ConcurrencyStamp = "b4c38214-364e-4c16-9028-a2d7448435b1",
                PhoneNumber = "09150000000",
                PhoneNumberConfirmed = true,
                TwoFactorEnabled = false,
                LockoutEnabled = false,
                AccessFailedCount = 0,
                Name = "Admin",
                Family = "Admin"
            }, new User()
            {
                Id = 2,
                UserName = "User1",
                NormalizedUserName = "USER1",
                PasswordHash = "AQAAAAEAACcQAAAAEEntwM294Ph6cmevAy6Q4LHVkhEEQC9Tqurw0jnG+J55aF0s2Ppsz4MRbR7ker9A8w==",
                SecurityStamp = "UC7D6KUHK3PXCOTP2CGX6L7IMP4TFWKM",
                ConcurrencyStamp = "8826450d-66b7-4a10-880a-cbad5790e841",
                PhoneNumber = "09150000000",
                PhoneNumberConfirmed = true,
                TwoFactorEnabled = false,
                LockoutEnabled = false,
                AccessFailedCount = 0,
                Name = "Admin",
                Family = "Admin",
            });
            #endregion  User
            #region  Role
            modelBuilder.Entity<Role>().HasData(new Role()
            {
                Id = 1,
                Name = "Administrator",
                NormalizedName = "ADMINISTRATOR",
                ConcurrencyStamp = "420d99de-7b2d-4a31-acd9-da52b0927bd0",
            }, new Role()
            {
                Id = 2,
                Name = "User",
                NormalizedName = "USER",
                ConcurrencyStamp = "47a274bd-9ea4-4475-a931-00ea4a3e86f7",
            });
            #endregion  User Role
            #region  User Role
            modelBuilder.Entity<UserRole>().HasData(
                new UserRole()
                {
                    RoleId = 1,
                    UserId = 1
                },
                new UserRole()
                {
                    RoleId = 2,
                    UserId = 2
                });
            #endregion  User Role
            #endregion Seed
        }
    }
}
