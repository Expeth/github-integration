using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;

namespace GithubIntegration.Host.Services.Fabrics
{
    public interface IHttpRequestFabric
    {
        HttpRequestMessage ConstructHttpRequestMessage(HttpMethod method, string uri);
    }
    
    public class HttpRequestWithIdentityHeadersFabric : IHttpRequestFabric
    {
        public HttpRequestMessage ConstructHttpRequestMessage(HttpMethod method, string uri)
        {
            var msg = new HttpRequestMessage(method, uri);
            var assemblyName = Assembly.GetExecutingAssembly().GetName();
            var productInfo = new ProductInfoHeaderValue(assemblyName.Name, assemblyName.Version.ToString());

            msg.Headers.UserAgent.Add(productInfo);
            
            return msg;
        }
    }
}