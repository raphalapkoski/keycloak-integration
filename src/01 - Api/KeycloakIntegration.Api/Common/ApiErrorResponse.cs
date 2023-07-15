using FluentResults;

namespace KeycloakIntegration.Api.Common
{
    public class ApiErrorResponse
    {
        public ApiErrorResponse(IReadOnlyCollection<Error> errors) => Errors = errors;

        public IReadOnlyCollection<Error> Errors { get; }
    }
}
