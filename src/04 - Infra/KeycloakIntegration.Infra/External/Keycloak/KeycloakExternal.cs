using KeycloakIntegration.Domain.External.Keycloak;
using Newtonsoft.Json;

namespace KeycloakIntegration.Infra.External.Keycloak;

internal class KeycloakExternal : IKeycloakExternal
{
    readonly HttpClient _httpClient;

    public KeycloakExternal(IHttpClientFactory httpClientFactory)
        => _httpClient = httpClientFactory.CreateClient("keycloak");

    public async Task<KeycloakAuthenticationResponse> Authentication(KeycloakAuthenticationRequest request)
    {
        var data = new[]
        {
            new KeyValuePair<string, string>("client_id", request.ClientId),
            new KeyValuePair<string, string>("client_secret", request.ClientSecret),
            new KeyValuePair<string, string>("grant_type", "client_credentials"),
        };

        var form = new FormUrlEncodedContent(data);

        var response = await _httpClient.PostAsync("/realms/keycloak/protocol/openid-connect/token", form);

        if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            throw new UnauthorizedAccessException();

        return JsonConvert.DeserializeObject<KeycloakAuthenticationResponse>(await response.Content.ReadAsStringAsync());
    }
}
