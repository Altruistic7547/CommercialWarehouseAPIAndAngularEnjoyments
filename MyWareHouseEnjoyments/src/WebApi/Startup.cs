using MyWarehouse.WebApi.ErrorHandling;
using MyWarehouse.WebApi.CORS;
using MyWarehouse.Infrastructure;
using MyWarehouse.Application;
using MyWarehouse.WebApi.Authentication;
using MyWarehouse.WebApi.Swagger;
using MyWarehouse.WebApi.Versioning;

namespace MyWarehouse.WebApi;

[ExcludeFromCodeCoverage]
public class Startup
{
    protected IConfiguration Configuration { get; }
    protected IWebHostEnvironment Environment { get; }

    public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        => (Configuration, Environment) = (configuration, environment);

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddMyApi();
        services.AddMyApiAuthDeps();
        services.AddMyErrorHandling();
        services.AddMySwagger(Configuration);
        services.AddMyVersioning();
        services.AddMyCorsConfiguration(Configuration);
        services.AddMyInfrastructureDependencies(Configuration, Environment);
        services.AddMyApplicationDependencies();
    }

    public void Configure(IApplicationBuilder app)
    {
        app.UseHttpsRedirection();
        app.UseRouting();
        app.UseMyCorsConfiguration();
        app.UseMySwagger(Configuration);
        app.UseMyInfrastructure(Configuration, Environment);
        app.UseMyApi();
    }
}
