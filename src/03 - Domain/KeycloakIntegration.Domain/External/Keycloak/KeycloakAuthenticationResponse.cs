using System.Text.Json.Serialization;

namespace KeycloakIntegration.Domain.External.Keycloak
{
    public record KeycloakAuthenticationResponse([property: JsonPropertyName("access_token")] string AccessToken);
}
