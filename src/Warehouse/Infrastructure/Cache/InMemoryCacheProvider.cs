using Application.Interfaces;
using Microsoft.Extensions.Caching.Memory;

namespace Infrastructure.Cache;

public class InMemoryCacheProvider : ICacheProvider
{
    private readonly IMemoryCache _memoryCache;

    public InMemoryCacheProvider(IMemoryCache memoryCache)
    {
        _memoryCache = memoryCache;
    }

    public T Get<T>(string cacheKey)
    {
        return (T)(_memoryCache.Get(cacheKey));
    }

    public void Set<T>(string cacheKey, T entity)
    {
        _memoryCache.Set(cacheKey, entity);
    }
}