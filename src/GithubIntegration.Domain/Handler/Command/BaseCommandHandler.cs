using System;
using System.Threading;
using System.Threading.Tasks;
using GithubIntegration.Domain.Abstraction.Fabric;
using MediatR;
using OneOf;

namespace GithubIntegration.Domain.Handler.Command
{
    public abstract class BaseRequest<TResponse> : IRequest<OneOf<TResponse, InternalError>>
    {
        public abstract string ToLogObj();
    }
    
    public abstract class BaseFailedResponse
    {
        public int ErrorCode { get; }
        public string Message { get; }

        public BaseFailedResponse(int errorCode, string message)
        {
            ErrorCode = errorCode;
            Message = message;
        }
    }

    public sealed class InternalError : BaseFailedResponse
    {
        private Exception? Exception { get; }

        public Exception? GetException() => Exception;
        
        public InternalError(int code, string message, Exception? exception) : base(code, message)
        {
            Exception = exception;
        }
    }

    internal abstract class
        BaseCommandHandler<TRequest, TResponse> : IRequestHandler<TRequest, OneOf<TResponse, InternalError>>
        where TRequest : BaseRequest<TResponse>
    {
        private readonly IResponseFabric _responseFabric;

        protected BaseCommandHandler(IResponseFabric responseFabric)
        {
            _responseFabric = responseFabric;
        }
        
        public async Task<OneOf<TResponse, InternalError>> Handle(TRequest request, CancellationToken cancellationToken)
        {
            try
            {
                return await HandleInternal(request, cancellationToken);
            }
            catch (Exception e)
            {
                return _responseFabric.ConstructInternalError(e);
            }
        }

        public abstract Task<OneOf<TResponse, InternalError>> HandleInternal(TRequest request,
            CancellationToken cancellationToken);
    }
}