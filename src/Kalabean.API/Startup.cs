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

namespace Kalabean.API
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
                .AddScoped<IRequirementRepository, RequirementRepository>()
                .AddScoped<IUserRepository, UserRepository>();
            services.
                AddFileProvider().
                AddMappers().
                AddServices();

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Kala", Version = "v1" });
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
