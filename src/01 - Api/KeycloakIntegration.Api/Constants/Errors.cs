using KeycloakIntegration.Application.Contracts.Error;

namespace KeycloakIntegration.Api.Constants
{
    internal static class Errors
    {
        internal static CustomError UnProcessableRequest => new("UnProcessableRequest", "The server could not process the request.");

        internal static CustomError ServerError => new("ServerError", "The server encountered an unrecoverable error.");
    }
}
