using AutoMapper;
using ExampleService.Api.Extensions;
using ExampleService.Api.Mapping;
using ExampleService.Api.Middleware;
using ExampleService.Persistence.Mapper;

namespace ExampleService.Api
{
    public class StartUp(IConfiguration configuration)
    {
        public IConfiguration Configuration { get; private set; } = configuration;
        public string ApiVersion => Configuration.GetValue<string>("ApiVersion") ?? throw new ArgumentException("Could not find ApiVersion in appsettings.json");
        public string AppName => Configuration.GetValue<string>("AppName") ?? throw new ArgumentException("Could not find AppName in appsettings.json");
        public string DisplayName => Configuration.GetValue<string>("DisplayName") ?? throw new ArgumentException("Could not find DisplayName in appsettings.json");

        public void ConfigureServices(IServiceCollection services)
        {
            // Auto Mapper Configurations
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new ExampleServiceApiMappingProfile());
                mc.AddProfile(new ExampleServiceDomainMappingProfile());
            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);

            services.AddSwagger(DisplayName, ApiVersion, AppName);
            services.AddBusinessLogic();
            services.AddDAL(Configuration);
            services.AddControllers();
            services.AddEndpointsApiExplorer();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<StartUp> logger)
        {
            logger.LogInformation("Configuring application.");

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseRouting();
            app.UseSwagger();
            app.UseSwaggerUI(c => {
                c.SwaggerEndpoint($"/swagger/{ApiVersion}/swagger.json", $"{DisplayName}:{ApiVersion}");
                c.RoutePrefix = "";
            });

            app.UseMiddleware<GlobalExceptionHandler>();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
