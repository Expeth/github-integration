using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using GithubIntegration.Domain.Abstraction.Agent;
using GithubIntegration.Domain.Entity;
using GithubIntegration.Host.Services.Fabrics;

namespace GithubIntegration.Host.Services.Agents
{
    public class GithubAgent : IGithubAgent
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpRequestFabric _httpRequestFabric;

        public GithubAgent(HttpClient httpClient, IHttpRequestFabric httpRequestFabric)
        {
            _httpClient = httpClient;
            _httpRequestFabric = httpRequestFabric;
        }
        
        public async Task<IEnumerable<RepositoryEntity>?> GetRepositories(string username)
        {
            var httpRequest = _httpRequestFabric.ConstructHttpRequestMessage(HttpMethod.Get, $"users/{username}/repos");
            var resp = await _httpClient.SendAsync(httpRequest);
            resp.EnsureSuccessStatusCode();
            
            var items = await resp.Content.ReadFromJsonAsync<IEnumerable<RepositoryInfoDTO>>();
            return items?.Select(RepositoryInfoDTO.Map);
        }

        private class RepositoryInfoDTO
        {
            public string name { get; set; }
            public string full_name { get; set; }
            public string html_url { get; set; }
            public string description { get; set; }
            public string clone_url { get; set; }

            public static RepositoryEntity Map(RepositoryInfoDTO dto) => new RepositoryEntity(dto.name, dto.full_name,
                dto.description, dto.html_url, dto.clone_url);
        }
    }
}