using Kalabean.Domain.Base;
using Kalabean.Domain.Repositories;
using Kalabean.Infrastructure;
using Kalabean.Infrastructure.Repositories;
using Kalabean.Infrastructure.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kalabean.Infrastructure.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Kalabean.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            JWTTokenManager.configuration = configuration;
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
            services.AddCors(options =>
            {
                options.AddPolicy("Kalabean", builder => builder
                    .SetIsOriginAllowed((host) => true)
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials());
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
                .AddScoped<IRequirementUserSeenRepository, RequirementUserSeenRepository>()
                .AddScoped<IRequirementRepository, RequirementRepository>()
                .AddScoped<IUserRepository, UserRepository>()
                .AddScoped<IArticleRepository, ArticleRepository>()
                .AddScoped<IAdvertiseRepository,AdvertiseRepository>()
                .AddScoped<IProductCommentRepository, ProductCommentRepository>()
                .AddScoped<IRolePermissionRepository, RolePermissionRepository>()
            .AddScoped<IPossibilitiesShopCenterRepository, PossibilitiesShopCenterRepository>();
            services.
                AddFileProvider().
                AddMappers().
                AddServices().
                AddMyIdentity().
                GetConfigs(Configuration);

            services.AddControllers();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Kala", Version = "v1" });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme."
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
         {
             {
                   new OpenApiSecurityScheme
                     {
                         Reference = new OpenApiReference
                         {
                             Type = ReferenceType.SecurityScheme,
                             Id = "Bearer"
                         }
                     },
                     new string[] {}
                    }
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
            }
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Kala v1"));

            //app.UseHttpsRedirection();
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new Microsoft.Extensions.FileProviders.PhysicalFileProvider(
                     System.IO.Path.Combine(env.ContentRootPath, "KL_ImagesRepo")),
                RequestPath = "/KL_ImagesRepo"
            });
            app.UseRouting();
            app.UseCors("Kalabean");
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
