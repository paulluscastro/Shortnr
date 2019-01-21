using Microsoft.Extensions.DependencyInjection;
using Shortnr.Api.Data;
using Shortnr.Api.Repositories;
using Shortnr.Api.Repositories.Interfaces;
using Shortnr.Api.Services;
using Shortnr.Api.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shortnr.Api
{
    public static class DependencyResolver
    {
        public static void Resolve(IServiceCollection services)
        {
            services.AddScoped<ApiDataContext, ApiDataContext>();
            services.AddScoped<IUrlRepository, UrlRepository>();
            services.AddScoped<IUrlService, UrlService>();
            services.AddScoped<IParameterRepository, ParameterRepository>();
            services.AddScoped<IParameterService, ParameterService>();
            services.AddScoped<UrlRepository, UrlRepository>();
            services.AddScoped<UrlService, UrlService>();
            services.AddScoped<ParameterRepository, ParameterRepository>();
            services.AddScoped<ParameterService, ParameterService>();
        }
    }
}
