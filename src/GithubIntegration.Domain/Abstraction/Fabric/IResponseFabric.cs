using System;
using GithubIntegration.Domain.Handler.Command;

namespace GithubIntegration.Domain.Abstraction.Fabric
{
    public interface IResponseFabric
    {
        InternalError ConstructInternalError(Exception? ex);
    }
}