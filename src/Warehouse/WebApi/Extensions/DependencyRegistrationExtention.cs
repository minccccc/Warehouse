namespace WebApi.Extensions;

public static class DependencyRegistrationExtention
{
    public static void AddPresentation(this IServiceCollection services)
    {
        services.AddMemoryCache();
        services.AddControllers();
        services.AddAutoMapper(typeof(Program));
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
    }
}
