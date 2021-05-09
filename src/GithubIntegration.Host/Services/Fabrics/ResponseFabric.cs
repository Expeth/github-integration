using System;
using GithubIntegration.Domain.Abstraction.Fabric;
using GithubIntegration.Domain.Handler.Command;
using GithubIntegration.Host.Constants;

namespace GithubIntegration.Host.Services.Fabrics
{
    public class ResponseFabric : IResponseFabric
    {
        public InternalError ConstructInternalError(Exception? ex) =>
            new InternalError(ResponseConsts.InternalServerError.code, ResponseConsts.InternalServerError.msg, ex);
    }
}