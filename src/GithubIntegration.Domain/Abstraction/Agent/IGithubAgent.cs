using System.Collections.Generic;
using System.Threading.Tasks;
using GithubIntegration.Domain.Entity;

namespace GithubIntegration.Domain.Abstraction.Agent
{
    public interface IGithubAgent
    {
        Task<IEnumerable<RepositoryEntity>?> GetRepositories(string username);
    }
}