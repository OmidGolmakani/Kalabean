using System.Reflection;
using Kalabean.Domain.Mappers;
using Kalabean.Domain.Services;
using Kalabean.Infrastructure.Files;
using Kalabean.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Kalabean.Infrastructure.Extensions
{
    public static class DependencyRegistration
    {
        public static IServiceCollection AddMappers(this IServiceCollection services)
        {
            services.AddSingleton<ICityMapper, CityMapper>()
                .AddSingleton<ICategoryMapper, CategoryMapper>()
                .AddSingleton<IAccessRuleMapper, AccessRuleMapper>()
                .AddSingleton<IShoppingCenterTypeMapper, ShoppingCenterTypeMapper>()
                .AddSingleton<IShoppingCenterMapper, ShoppingCenterMapper>()
                .AddSingleton<IFloorMapper, FloorMapper>()
                .AddSingleton<IStoreMapper, StoreMapper>();
            return services;
        }

        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services
                .AddScoped<ICityService, CityService>()
                .AddScoped<IAccessRulesService, AccessRulesService>()
                .AddScoped<ICategoryService, CategoryService>()
                .AddScoped<IShoppingCenterTypeService, ShoppingCenterTypeService>()
                .AddScoped<IShoppingCenterService, ShoppingCenterService>()
                .AddScoped<IFloorService, FloorService>()
                .AddScoped<IStoreService, StoreService>();

            return services;
        }

        public static IServiceCollection AddFileProvider(this IServiceCollection services)
        {
            services
                .AddScoped<IFileAccessProvider, FileAccessProvider>();
            return services;
        }
    }
}
