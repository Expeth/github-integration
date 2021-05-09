using System.Collections.Generic;
using GithubIntegration.Contracts.Model;

namespace GithubIntegration.Contracts.Response
{
    public sealed class GetRepositoriesResponse
    {
        public IEnumerable<RepositoryInfo> Repositories { get; set; }

        public GetRepositoriesResponse() { }

        public GetRepositoriesResponse(IEnumerable<RepositoryInfo> repositories)
        {
            Repositories = repositories;
        }
    }
}