using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using GithubIntegration.Host.Constants;
using Microsoft.Extensions.Configuration;

namespace GithubIntegration.Host.Services.Fabrics
{
    public interface IHttpRequestFabric
    {
        HttpRequestMessage ConstructHttpRequestMessage(HttpMethod method, string uri);
    }
    
    public class HttpRequestWithIdentityHeadersFabric : IHttpRequestFabric
    {
        private readonly IConfiguration _cfg;

        public HttpRequestWithIdentityHeadersFabric(IConfiguration cfg)
        {
            _cfg = cfg;
        }
        
        public HttpRequestMessage ConstructHttpRequestMessage(HttpMethod method, string uri)
        {
            var msg = new HttpRequestMessage(method, uri);

            var apiToken = _cfg.GetSection(ConfigurationConsts.GithubApiToken).Get<string>();
            var assemblyName = Assembly.GetExecutingAssembly().GetName();
            var productInfo = new ProductInfoHeaderValue(assemblyName.Name, assemblyName.Version.ToString());
            var authorization = new AuthenticationHeaderValue("token", apiToken);

            msg.Headers.UserAgent.Add(productInfo);
            msg.Headers.Authorization = authorization;
            
            return msg;
        }
    }
}