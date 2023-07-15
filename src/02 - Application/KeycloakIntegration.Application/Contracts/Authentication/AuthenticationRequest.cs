using System.Text.Json.Serialization;

namespace KeycloakIntegration.Application.Contracts.Authentication;

public sealed record AuthenticationRequest([property: JsonPropertyName("client_id")] string ClientId,
                                           [property: JsonPropertyName("client_secret")] string ClientSecret);
