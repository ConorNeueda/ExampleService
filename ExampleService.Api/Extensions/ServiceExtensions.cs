using ExampleService.Domain.Interfaces;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using ExampleService.Persistence.Context;
using ExampleService.Persistence.Repositories;
using ExampleService.Business.Services;

namespace ExampleService.Api.Extensions
{
    public static class ServiceExtensions
    {
        /// <summary>
        /// Adds swagger to the service collection
        /// </summary>
        /// <param name="services"></param>
        /// <param name="displayName"></param>
        /// <param name="apiVersion"></param>
        /// <param name="appName"></param>
        public static void AddSwagger(this IServiceCollection services, string displayName, string apiVersion, string appName)
        {
            services.AddSwaggerGen(opt => {
                opt.SwaggerDoc(apiVersion, new Microsoft.OpenApi.Models.OpenApiInfo { Title = displayName, Version = apiVersion });
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                opt.IncludeXmlComments(xmlPath);

            });
        }

        /// <summary>
        /// Adds business services to the service collection
        /// </summary>
        /// <param name="services"></param>
        public static void AddBusinessLogic(this IServiceCollection services)
        {
            services.AddScoped<IExampleService, ExampleBusinessService>();
        }

        /// <summary>
        /// Adds database related services to the service collection
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <exception cref="ArgumentException"></exception>
        public static void AddDAL(this IServiceCollection services, IConfiguration configuration)
        {
            var magentaConnectionString = configuration.GetConnectionString("MagentaDbContext")
                ?? throw new ArgumentException("'MagentaDbContext' connection string is not specified in the configuration", nameof(configuration));

            services.AddDbContext<MagentaDbContext>(opt => opt.UseLazyLoadingProxies().UseSqlServer(magentaConnectionString));

            //Add repositories
            services.AddScoped<IExampleRepository, ExampleRepository>();
        }
    }
}
