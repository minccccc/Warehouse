using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Http.Configuration
{
    public static class AddHttpConfigExtensions
    {
        public static void AddHttpConfiguration(this IServiceCollection services)
        {
            services.AddHttpClient();
            services.AddTransient<IRetrieveProductsService, RetrieveProductsService>();
        }
    }
}
