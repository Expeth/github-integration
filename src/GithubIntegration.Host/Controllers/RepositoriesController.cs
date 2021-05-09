using System.Linq;
using System.Threading.Tasks;
using GithubIntegration.Contracts.Model;
using GithubIntegration.Contracts.Response;
using GithubIntegration.Domain.Handler.Command;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GithubIntegration.Host.Controllers
{
    [ApiController]
    [Route("repositories")]
    public class RepositoriesController : GeneralController
    {
        public RepositoriesController(IMediator mediator) : base(mediator) { }
        
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var request = new GetRepositories.Request("expeth");
            return await ProcessRequest<GetRepositories.Request, GetRepositories.Response, GetRepositoriesResponse>(
                request, Map);
        }

        private GetRepositoriesResponse Map(GetRepositories.Response resp) => new GetRepositoriesResponse(
            resp.Entities.Select(entity =>
                new RepositoryInfo(entity.Name, entity.FullName, entity.HtmlUrl, entity.CloneUrl, entity.Description)));
    }
}