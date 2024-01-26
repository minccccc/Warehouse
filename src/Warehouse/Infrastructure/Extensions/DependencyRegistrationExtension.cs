using Infrastructure.Cache;
using Infrastructure.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;

namespace Infrastructure.Extensions;

public static class DependencyRegistrationExtention
{
    public static void AddInfrastructure(this IServiceCollection services)
    {
        services.AddHttpClient();
        services.AddTransient<IRetrieveProductsService, RetrieveProductsService>();
        services.AddSingleton<ICacheProvider, InMemoryCacheProvider>();
    }

    public static void AddLogger(this ILoggingBuilder loggingBuilder, IConfiguration configuration)
    {
        var logger = new LoggerConfiguration()
            .ReadFrom.Configuration(configuration)
            .Enrich.FromLogContext()
            .CreateLogger();

        loggingBuilder.ClearProviders();
        loggingBuilder.AddSerilog(logger);
    }
}