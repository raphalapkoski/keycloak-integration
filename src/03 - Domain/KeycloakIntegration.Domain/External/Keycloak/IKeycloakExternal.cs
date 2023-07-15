namespace KeycloakIntegration.Domain.External.Keycloak
{
    public interface IKeycloakExternal
    {
        Task<KeycloakAuthenticationResponse> Authentication(KeycloakAuthenticationRequest request);
    }
}
