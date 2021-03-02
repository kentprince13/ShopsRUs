using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using ShopsRUs.API.Filter;
using ShopsRUs.API.Infrastructure.AutomapperConfig;
using ShopsRUs.Core.Infrastructure;
using ShopsRUs.Infrastructure;
using ShopsRUs.Infrastructure.Services.CustomerService;
using ShopsRUs.Infrastructure.Services.DiscountService;
using ShopsRUs.Infrastructure.Services.InvoiceService;
using ShopsRUs.Infrastructure.Services.UserService;

namespace ShopsRUs.API
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
            services.AddTransient<ICustomerService, CustomerService>();
            services.AddTransient<IDiscountService, DiscountService>();
            services.AddTransient<IInvoiceServices, InvoiceService>();
            services.AddTransient<IUsersService, UsersService>();
            services.AddTransient<IInvoicingService, InvoicingService>();

            var config = new MapperConfiguration(configure => { configure.AddProfile(new MappingProfile()); });
            var mapper = config.CreateMapper();
            services.AddSingleton(mapper);

            var connectionsString = "Data Source=shopurs.db";
            services.AddDbContext<ShopsRUsContext>(optionsAction: c =>
            {
                c.UseSqlite(connectionsString,
                    d => { d.MigrationsAssembly(typeof(ShopsRUsContext).GetTypeInfo().Assembly.GetName().Name); });
            });
            services.AddMvc(c => { c.Filters.Add(typeof(GlobalCustomExceptionFilter)); })
                .AddFluentValidation(c => { c.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly()); });

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ShopsRUs.API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ShopsRUs.API v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
