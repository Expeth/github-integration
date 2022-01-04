using System;
using System.Threading;
using System.Threading.Tasks;
using GithubIntegration.Domain.Abstraction;
using GithubIntegration.Domain.Abstraction.Agent;
using GithubIntegration.Domain.Abstraction.Fabric;
using GithubIntegration.Domain.Entity;
using OneOf;

namespace GithubIntegration.Domain.Handler.Command
{
    public static class GetLatestRelease
    {
        public sealed class Request : BaseRequest<Response>
        {
            public string Username { get; }
            public string Repository { get; }

            public Request(string username, string repository)
            {
                Username = username;
                Repository = repository;
            }

            public override string ToLogObj()
            {
                return $"Username = {Username} Repository = {Repository}";
            }
        }

        public sealed class Response
        {
            public ReleaseEntity? ReleaseEntity { get; }

            public Response(ReleaseEntity? releaseEntity)
            {
                ReleaseEntity = releaseEntity;
            }
        }

        internal sealed class Handler : BaseCommandHandler<Request, Response>
        {
            private readonly IGithubAgent _githubAgent;
            private readonly IInMemoryCache<ReleaseEntity?> _cache;

            public Handler(IResponseFabric responseFabric,
                IInMemoryCache<ReleaseEntity?> cache,
                IGithubAgent githubAgent) : base(responseFabric)
            {
                _cache = cache;
                _githubAgent = githubAgent;
            }

            public override async Task<OneOf<Response, InternalError>> HandleInternal(Request request,
                CancellationToken cancellationToken)
            {
                var cacheKey = $"{request.Username}/{request.Repository}";
                var cacheResult = await _cache.GetItemAsync(cacheKey);

                if (cacheResult != null)
                    return new Response(cacheResult);
                
                var apiResponse = await _githubAgent.GetLatestRelease(request.Username, request.Repository);
                _cache.SetItem(cacheKey, apiResponse, TimeSpan.FromMinutes(10));

                return new Response(apiResponse);
            }
        }
    }
}