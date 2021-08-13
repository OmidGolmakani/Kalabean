using System.Reflection;
using Kalabean.Domain.Mappers;
using Kalabean.Domain.Services;
using Kalabean.Infrastructure.Files;
using Kalabean.Infrastructure.Services;
using Kalabean.Infrastructure.Services.Image;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace Kalabean.Infrastructure.Extensions
{
    public static class DependencyRegistration
    {
        public static IServiceCollection AddMappers(this IServiceCollection services)
        {
            services.AddSingleton<ICityMapper, CityMapper>()
                .AddSingleton<ICategoryMapper, CategoryMapper>()
                .AddSingleton<IShoppingCenterTypeMapper, ShoppingCenterTypeMapper>()
                .AddSingleton<IShoppingCenterMapper, ShoppingCenterMapper>()
                .AddSingleton<IFloorMapper, FloorMapper>()
                .AddSingleton<IStoreMapper, StoreMapper>()
                .AddSingleton<IProductMapper, ProductMapper>()
                .AddSingleton<IOrderHeaderMapper, OrderHeaderMapper>()
                .AddSingleton<IOrderDetailMapper, OrderDetailMapper>()
                .AddSingleton<IProductImageMapper, ProductImageMapper>()
                .AddSingleton<IOrderHeaderMapper, OrderHeaderMapper>()
                .AddSingleton<IRequirementMapper, RequirementMapper>()
                .AddSingleton<IUserMapper, UserMapper>()
                .AddSingleton<IRolePermissionMapper, RolePermissionMapper>()
                .AddSingleton<IArticleMapper, ArticleMapper>(); ;
            return services;
        }

        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            #region Data Servicees
            services
                    .AddScoped<ICityService, CityService>()
                    .AddScoped<ICategoryService, CategoryService>()
                    .AddScoped<IShoppingCenterTypeService, ShoppingCenterTypeService>()
                    .AddScoped<IShoppingCenterService, ShoppingCenterService>()
                    .AddScoped<IFloorService, FloorService>()
                    .AddScoped<IStoreService, StoreService>()
                    .AddScoped<IProductService, ProductService>()
                    .AddScoped<IOrderService, OrderService>()
                    .AddScoped<IRequirementService, RequirementService>()
                    .AddScoped<IUserService, UserService>()
                    .AddScoped<IRolePermissionService, RolePermissionService>();
            #endregion Data Services
            #region Other Services
            services.AddScoped<IResizeImageService, ResizeImageService>();
            #endregion Other Servises

            return services;
        }

        public static IServiceCollection AddFileProvider(this IServiceCollection services)
        {
            services
                .AddScoped<IFileAccessProvider, FileAccessProvider>();
            return services;
        }
        public static IServiceCollection AddMyIdentity(this IServiceCollection services)
        {
            services.AddIdentity<Kalabean.Domain.Entities.User, Kalabean.Domain.Entities.Role>(config =>
            {
                config.Password.RequireDigit = false;
                //config.Password.RequiredLength = 10;
                config.Password.RequireLowercase = false;
                config.Password.RequireUppercase = false;
                config.Password.RequireNonAlphanumeric = false;
                config.User.RequireUniqueEmail = false;
                config.SignIn.RequireConfirmedEmail = false;
                config.SignIn.RequireConfirmedPhoneNumber = true;
            })
            .AddEntityFrameworkStores<AppDbContext>()
            .AddUserStore<Kalabean.Domain.Entities.ApplicationUserStore>()
            .AddRoles<Kalabean.Domain.Entities.Role>()
            .AddDefaultTokenProviders();
            return services;
        }
    }
}
