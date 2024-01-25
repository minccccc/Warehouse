using Application.PipelineBehaviors;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Configuration
{
    public static class MediatrConfigurationExtensions
    {
        public static void AddMediatrConfiguration(this IServiceCollection services)
        {
            services.AddMediatR(cfg =>
                cfg.RegisterServicesFromAssembly(typeof(MediatrConfigurationExtensions).Assembly));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        }
    }
}
