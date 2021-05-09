using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GithubIntegration.Domain.Abstraction;
using GithubIntegration.Domain.Entity;
using Microsoft.Extensions.Caching.Memory;

namespace GithubIntegration.Host.Services
{
    internal class InMemoryCache<T> : IInMemoryCache<T>
    {
        private readonly IMemoryCache _cache;

        public InMemoryCache(IMemoryCache cache)
        {
            _cache = cache;
        }
        
        public T GetItem(string key)
        {
            return _cache.Get<T>(key);
        }

        public Task<T> GetItemAsync(string key)
        {
            return Task.FromResult(GetItem(key));
        }

        public bool SetItem(string key, T value, TimeSpan ttl)
        {
            try
            {
                _cache.Set(key, value, ttl);
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }
    }
    
    internal class RepositoriesStorage : InMemoryCache<IEnumerable<RepositoryEntity>?>
    {
        public RepositoriesStorage(IMemoryCache cache) : base(cache) { }
    }
}