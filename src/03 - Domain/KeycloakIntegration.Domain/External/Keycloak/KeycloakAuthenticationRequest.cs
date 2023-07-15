namespace KeycloakIntegration.Domain.External.Keycloak
{
    public record KeycloakAuthenticationRequest(string ClientId, 
                                                string ClientSecret);
}
