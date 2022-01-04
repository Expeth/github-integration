using System.Threading.Tasks;
using GithubIntegration.Contracts.Response;
using GithubIntegration.Domain.Handler.Command;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GithubIntegration.Host.Controllers
{
    [ApiController]
    [Route("badges")]
    public class BadgesController : GeneralController
    {
        public BadgesController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet("latestRelease/{repository}")]
        public async Task<IActionResult> Get([FromRoute]string repository)
        {
            var request = new GetLatestRelease.Request("expeth", repository);
            return await ProcessRequest<GetLatestRelease.Request, GetLatestRelease.Response, GetBadgeResponse>(
                request, Map);
        }

        private GetBadgeResponse Map(GetLatestRelease.Response resp) =>
            new GetBadgeResponse("1", "release", resp.ReleaseEntity?.Name, "blue");
    }
}