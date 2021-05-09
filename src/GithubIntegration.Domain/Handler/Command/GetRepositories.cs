using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
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
            private readonly IGithubAgent _agent;
            
            public Handler(IResponseFabric responseFabric, IGithubAgent agent) : base(responseFabric)
            {
                _agent = agent;
            }

            public override async Task<OneOf<Response, InternalError>> HandleInternal(Request request,
                CancellationToken cancellationToken)
            {
                var result = await _agent.GetRepositories(request.Username);
                return new Response(result ?? Enumerable.Empty<RepositoryEntity>());
            }
        }
    }
}