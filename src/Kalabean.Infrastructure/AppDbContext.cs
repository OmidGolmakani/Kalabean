using System;
using Microsoft.EntityFrameworkCore;
using Kalabean.Domain.Entities;
using Kalabean.Infrastructure.Extensions.SchemaDefinitions;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

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
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
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


            base.OnModelCreating(modelBuilder);
        }
    }
}
