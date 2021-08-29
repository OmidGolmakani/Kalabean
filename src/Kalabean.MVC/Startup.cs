using Kalabean.Domain.Base;
using Kalabean.Domain.Repositories;
using Kalabean.Infrastructure;
using Kalabean.Infrastructure.Extensions;
using Kalabean.Infrastructure.Helpers;
using Kalabean.Infrastructure.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kalabean.MVC
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            MyAppContext.Configure();
            services.AddEntityFrameworkSqlServer()
                .AddDbContext<AppDbContext>(options =>
                {
                    options.UseSqlServer(Configuration["Data:ConnectionString"],
                        options => options.MigrationsAssembly(typeof(Startup).Assembly.FullName));
                });

            services.AddScoped<Func<AppDbContext>>((provider) => () => provider.GetService<AppDbContext>());
            services.AddScoped<DbFactory>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>)).
                AddScoped<ICityRepository, CityRepository>()
                .AddScoped<ICategoryRepository, CategoryRepository>()
                .AddScoped<IShoppingCenterTypeRepository, ShoppingCenterTypeRepository>()
                .AddScoped<IShoppingCenterRepository, ShoppingCenterRepository>()
                .AddScoped<IFloorRepository, FloorRepository>()
                .AddScoped<IStoreRepository, StoreRepository>()
                .AddScoped<IProductRepository, ProductRepository>()
                .AddScoped<IProductImageRepository, ProductImageRepository>()
                .AddScoped<IOrderHeaderRepository, OrderHeaderRepository>()
                .AddScoped<IOrderDetailRepository, OrderDetailRepository>()
                .AddScoped<IRequirementRepository, RequirementRepository>()
                .AddScoped<IUserRepository, UserRepository>()
                .AddScoped<IArticleRepository, ArticleRepository>()
                .AddScoped<IAdvertiseRepository, AdvertiseRepository>()
                .AddScoped<IProductCommentRepository, ProductCommentRepository>()
                .AddScoped<IRolePermissionRepository, RolePermissionRepository>()
            .AddScoped<IPossibilitiesShopCenterRepository, PossibilitiesShopCenterRepository>();
            services.
                AddFileProvider().
                AddMappers().
                AddServices().
                AddMyIdentity().
                GetConfigs(Configuration);

            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                //app.UseHsts();
            }

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute("Home", "",
                    new { controller = "Home", action = "Index" });

                endpoints.MapControllerRoute("ShoppingCenters", "shopping-centers/{typeName}-{typeId}",
                    new { controller = "ShoppingCenters", action = "ShoppingCenters" });

                endpoints.MapControllerRoute("StoresByShoppingCenter", "shopping-centers/{typeName}-{typeId}/{name}-{id}",
                    new { controller = "ShoppingCenters", action = "Stores" });

                endpoints.MapControllerRoute("Store", "store/{name}-{id}",
                    new { controller = "Stores", action = "StoreProfile" });

                endpoints.MapControllers();
            });
        }
    }
}
