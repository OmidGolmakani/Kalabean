using System;
using Microsoft.EntityFrameworkCore;
using Kalabean.Domain.Entities;
using Kalabean.Infrastructure.Extensions.SchemaDefinitions;

namespace Kalabean.Infrastructure
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        //public DbSet<Category> Categories{ get; set; }
        //public DbSet<Article> Articles { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<AccessRule> AccessRules { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ShoppingCenterType> ShoppingCenterTypes { get; set; }
        public DbSet<ShoppingCenter> ShoppingCenters { get; set; }
        public DbSet<Floor> Floors { get; set; }
        public DbSet<Store> Stores { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new RuleEntitySchemaDefinition());
            modelBuilder.ApplyConfiguration(new CategoryEntitySchemaDefinition());
            modelBuilder.ApplyConfiguration(new CityEntitySchemaDefinition());
            modelBuilder.ApplyConfiguration(new ShoppingCenterTypeEntitySchemaDefinition());
            modelBuilder.ApplyConfiguration(new ShoppingCenterEntitySchemaDefinition());
            modelBuilder.ApplyConfiguration(new FloorEntitySchemaDefinition());
            modelBuilder.ApplyConfiguration(new StoreEntitySchemaDefinition());
            base.OnModelCreating(modelBuilder);
        }
    }
}
