using FluentResults;
using KeycloakIntegration.Application.Abstractions;
using KeycloakIntegration.Application.Contracts.Authentication;
using KeycloakIntegration.Domain.External.Keycloak;

namespace KeycloakIntegration.Application.Core.Authentication;

internal sealed class AuthenticationCommandHandler : ICommandHandler<AuthenticationCommand, Result<AuthenticationResponse>>
{
    readonly IKeycloakExternal _keycloakExternal;

    public AuthenticationCommandHandler(IKeycloakExternal keycloakExternal)
    {
        _keycloakExternal = keycloakExternal;
    }

    public async Task<Result<AuthenticationResponse>> Handle(AuthenticationCommand command, CancellationToken cancellationToken)
    {
        var keycloakAuthenticationRequest = GetKeycloakAuthenticationRequest(command);
        var result = await _keycloakExternal.Authentication(keycloakAuthenticationRequest);
        AuthenticationResponse response = GetAuthenticationResponse(result);
        return Result.Ok(response);
    }

    private static KeycloakAuthenticationRequest GetKeycloakAuthenticationRequest(AuthenticationCommand command)
        => new(command.ClientId, command.ClientSecret);

    private static AuthenticationResponse GetAuthenticationResponse(KeycloakAuthenticationResponse result)
        => new(result.AccessToken);
}
