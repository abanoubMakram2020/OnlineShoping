using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using OnlineShoping.Application.Validations;
using OnlineShoping.Infra.IOC;
using SharedKernal.Middlewares;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShoping.Presentation.API
{
    public class Startup
    {
        public IConfiguration _configuration { get; }
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
            _configuration.Initialize();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers()
                 .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<ProductInputValidation>(lifetime: ServiceLifetime.Scoped))
                 .AddControllersAsServices();
            services.Initialize();
            RegisterServices(services, _configuration);

            services.AddApiVersioning(x =>
            {
                x.DefaultApiVersion = new ApiVersion(1, 0);
                x.AssumeDefaultVersionWhenUnspecified = true;
                x.ReportApiVersions = true;
            });

        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.Initialize();
            app.UseEndpoints(endpoints =>
            {

                endpoints.MapAreaControllerRoute(
                   name: "AdminPanelArea",
                   areaName: "AdminPanel",
                   pattern: "AdminPanel/{controller=Home}/{action=GetAll}/{id?}");

                endpoints.MapControllers();
            });
        }
        private static void RegisterServices(IServiceCollection services, IConfiguration _configuration)
        {
            DependencyContainer.RegisterServices(services, _configuration);

        }
    }
}
