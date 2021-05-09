using System;
using System.Collections.Generic;
using System.Net.Http;
using GithubIntegration.Domain.Abstraction;
using GithubIntegration.Domain.Abstraction.Agent;
using GithubIntegration.Domain.Abstraction.Fabric;
using GithubIntegration.Domain.Entity;
using GithubIntegration.Host.Configs;
using GithubIntegration.Host.Constants;
using GithubIntegration.Host.Services;
using GithubIntegration.Host.Services.Agents;
using GithubIntegration.Host.Services.Background;
using GithubIntegration.Host.Services.Fabrics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace GithubIntegration.Host.Extensions
{
    public static class ServiceCollectionExt
    {
        public static IServiceCollection AddFabrics(this IServiceCollection svc) =>
            svc.AddSingleton<IResponseFabric, ResponseFabric>()
                .AddSingleton<IHttpRequestFabric, HttpRequestWithIdentityHeadersFabric>();

        public static IHttpClientBuilder AddHttpClients(this IServiceCollection svc)
        {
            return svc.AddHttpClient<IGithubAgent, GithubAgent>((sp, client) =>
            {
                var cfg = sp.GetRequiredService<IOptions<HttpConfig>>().Value;

                client.BaseAddress = new Uri(cfg.Uri);
                client.Timeout = cfg.Timeout;
            }).ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
            {
                ClientCertificateOptions = ClientCertificateOption.Manual,
                ServerCertificateCustomValidationCallback =
                    (httpRequestMessage, cert, cetChain, policyErrors) => true
                    
            });
        }

        public static IServiceCollection AddBackgroundServices(this IServiceCollection svc) =>
            svc.AddHostedService<PullUpdatesHostedService>();
        
        public static IServiceCollection AddInMemoryStorages(this IServiceCollection svc) =>
            svc.AddMemoryCache().AddSingleton<IInMemoryCache<IEnumerable<RepositoryEntity>?>, RepositoriesStorage>();

        public static IServiceCollection AddConfigs(this IServiceCollection svc, IConfiguration cfg) =>
            svc.Configure<HttpConfig>(cfg.GetSection(ConfigurationConsts.GithubApiHttpCfg))
                .Configure<PullUpdatesConfig>(cfg.GetSection(ConfigurationConsts.GithubApiPullUpdates));
    }
}