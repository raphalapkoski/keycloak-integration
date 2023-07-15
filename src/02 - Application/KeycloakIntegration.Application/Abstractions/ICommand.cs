using MediatR;

namespace KeycloakIntegration.Application.Abstractions
{
    public interface ICommand<out TResponse> : IRequest<TResponse>
    {
    }
}
