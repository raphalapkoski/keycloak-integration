using MediatR;

namespace KeycloakIntegration.Application.Abstractions
{
    public interface ICommandHandler<in TCommand, TResponse> : IRequestHandler<TCommand, TResponse>
         where TCommand : ICommand<TResponse>
    {
    }
}
