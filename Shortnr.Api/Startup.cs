using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Shortnr.Api.Data;

namespace Shortnr.Api
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
            services.AddDbContext<ApiDataContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection"),
                    m => m.MigrationsHistoryTable("__ApiMigrationsHistory", "Shortnr")));
            DependencyResolver.Resolve(services);
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddSwaggerDocument(config =>
            {
                config.PostProcess = document =>
                {
                    document.Info.Version = "v1";
                    document.Info.Title = "Shortnr API";
                    document.Info.Description = "A simple URL shortnr designed to demonstrate ASP.NET MVC & Web API knowledge and to start learning about Docker as well.";
                    document.Info.TermsOfService = "This software as developed for study purposes only. You are free to take as it is and use it on your own responsability.";
                    document.Info.Contact = new NSwag.SwaggerContact
                    {
                        Name = "Paullus Nava",
                        Email = "paullus.castro@gmail.com"
                    };
                    document.Info.License = new NSwag.SwaggerLicense
                    {
                        Name = "The Unlicense",
                        Url = @"https://choosealicense.com/licenses/unlicense/"
                    };
                };
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseSwagger();
            app.UseSwaggerUi3();
            app.UseMvc();
        }
    }
}
