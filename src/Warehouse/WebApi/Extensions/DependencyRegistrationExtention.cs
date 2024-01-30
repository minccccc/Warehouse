using Application.Common.Constants;
using Application.Configuration;

namespace WebApi.Extensions;

public static class DependencyRegistrationExtention
{
    public static void AddConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        var mockyClientSection = configuration.GetSection(AppConstants.Configuration.MockyClientSection);
        services.Configure<MockyClientConfig>(mockyClientSection);

        var schedulerSection = configuration.GetSection(AppConstants.Configuration.SchedulerSection);
        services.Configure<SchedulerConfig>(schedulerSection);
    }

    public static void AddPresentation(this IServiceCollection services)
    {
        services.AddMemoryCache();
        services.AddControllers();
        services.AddAutoMapper(typeof(Program));
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
    }
}
