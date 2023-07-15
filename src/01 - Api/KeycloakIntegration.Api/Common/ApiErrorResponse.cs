

using KeycloakIntegration.Application.Contracts.Error;

namespace KeycloakIntegration.Api.Common
{
    public class ApiErrorResponse
    {
        public ApiErrorResponse(IReadOnlyCollection<CustomError> errors) => Errors = errors;

        public IReadOnlyCollection<CustomError> Errors { get; }
    }
}
