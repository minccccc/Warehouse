using Infrastructure.Cache;

namespace WebApi.Configuration
{
    public static class MemoryCacheExtensions
    {
        public static void AddMemoryCacheConfiguration(this IServiceCollection services)
        {
            services.AddMemoryCache();

            services.AddSingleton<ICacheProvider, InMemoryCacheProvider>();
        }
    }
}
