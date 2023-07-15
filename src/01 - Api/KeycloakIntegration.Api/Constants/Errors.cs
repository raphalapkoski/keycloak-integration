using FluentResults;

namespace KeycloakIntegration.Api.Constants
{
    internal static class Errors
    {
        internal static Error UnProcessableRequest => new("The server could not process the request.");

        internal static Error ServerError => new("The server encountered an unrecoverable error.");
    }
}
