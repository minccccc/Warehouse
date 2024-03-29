﻿namespace Application.Interfaces;

public interface ICacheProvider
{
    T Get<T>(string cacheKey);

    void Set<T>(string cacheKey, T entity);
}