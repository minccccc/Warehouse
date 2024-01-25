using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Configuration
{
    public static class AddFluentValidatorConfigExtensions
    {
        public static void AddFluentValidatorConfiguration(this IServiceCollection services)
        {
            services.AddValidatorsFromAssembly(typeof(AddFluentValidatorConfigExtensions).Assembly);
        }
    }
}
