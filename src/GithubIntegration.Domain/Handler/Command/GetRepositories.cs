using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GithubIntegration.Domain.Abstraction;
using GithubIntegration.Domain.Abstraction.Agent;
using GithubIntegration.Domain.Abstraction.Fabric;
using GithubIntegration.Domain.Entity;
using MediatR;
using OneOf;

namespace GithubIntegration.Domain.Handler.Command
{
    public static class GetRepositories
    {
        public sealed class Request : BaseRequest<Response>
        {
            public string Username { get; }

            public Request(string username)
            {
                Username = username;
            }

            public override string ToLogObj()
            {
                return $"Username = {Username}";
            }
        }

        public sealed class Response
        {
            public IEnumerable<RepositoryEntity> Entities { get; }

            public Response(IEnumerable<RepositoryEntity> entities)
            {
                Entities = entities;
            }
        }

        internal sealed class Handler : BaseCommandHandler<Request, Response>
        {
            private readonly IInMemoryCache<IEnumerable<RepositoryEntity>?> _cache;

            public Handler(IResponseFabric responseFabric, IInMemoryCache<IEnumerable<RepositoryEntity>> cache) : base(
                responseFabric)
            {
                _cache = cache;
            }

            public override async Task<OneOf<Response, InternalError>> HandleInternal(Request request,
                CancellationToken cancellationToken)
            {
                var result = await _cache.GetItemAsync(request.Username);
                return new Response(result ?? Enumerable.Empty<RepositoryEntity>());
            }
        }
    }
}