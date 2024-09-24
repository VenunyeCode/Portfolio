using Portflio.Profile;
using Portflio.Services;

namespace Portflio.Extensions;

public static class ServiceExtensions
{
    public static void ConfigureCors(this IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddPolicy("CorsPolicy",
                builder => builder.AllowAnyHeader()
                    .AllowAnyOrigin()
                    .WithMethods("PUT", "GET", "DELETE", "POST"));
        });
    }

    public static void ConfigureDbContextService(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<PortfolioContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("PortfolioContext") ?? throw new InvalidOperationException("Connection string was not found"));
        });
    }

    public static void ConfigureLoggerService(this IServiceCollection services)
    {
        services.AddSingleton<ILoggerManager, LoggerManager>();
    }

    public static void ConfigureAutoMapper(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(MappingProfile));
    }

    public static void ConfigureUnitOfWorkService(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
    }

    public static void ConfigureApiService(this IServiceCollection services)
    {
        services.AddTransient<IPlatformService, PlatformService>();
        services.AddTransient<IProjectService, ProjectService>();
        services.AddTransient<IProjectTypeService, ProjectTypeService>();
        services.AddTransient<ITechnologyService, TechnologyService>();
    }

    public static void ConfigureDetectionService(this IServiceCollection services)
    {
        
    }
}