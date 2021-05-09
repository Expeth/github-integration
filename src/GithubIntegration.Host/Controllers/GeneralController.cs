using System;
using System.Threading.Tasks;
using GithubIntegration.Domain.Handler.Command;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace GithubIntegration.Host.Controllers
{
    public class GeneralController : ControllerBase
    {
        private static readonly ILogger Logger = Log.ForContext<GeneralController>();
        
        private readonly IMediator _mediator;

        public GeneralController(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<IActionResult> ProcessRequest<TDomainRequest, TDomainResponse, TSvcResponse>(
            TDomainRequest request, Func<TDomainResponse, TSvcResponse> map)
            where TDomainRequest : BaseRequest<TDomainResponse>
        {
            Logger.Debug("Processing Request: {request}", request.ToLogObj());
            var domainResp = await _mediator.Send(request);
            return domainResp.Match(success => Ok(map(success)),
                internalError =>
                {
                    Logger.Error("Msg: {msg} Exc: {@exc}", internalError.Message, internalError.GetException());
                    return StatusCode(StatusCodes.Status500InternalServerError, internalError);
                });
        }
    }
}