using System;
using System.Threading.Tasks;

namespace GithubIntegration.Domain.Abstraction
{
    public interface IInMemoryCache<T>
    {
        T GetItem(string key);
        Task<T> GetItemAsync(string key);
        bool SetItem(string key, T value, TimeSpan ttl);
    }
}