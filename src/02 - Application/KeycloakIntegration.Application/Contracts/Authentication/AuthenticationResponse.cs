using System.Text.Json.Serialization;

namespace KeycloakIntegration.Application.Contracts.Authentication
{
    public sealed record AuthenticationResponse([property: JsonPropertyName("access_token")] string AccessToken);
}
