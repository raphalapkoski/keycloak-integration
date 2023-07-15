using FluentResults;
using KeycloakIntegration.Application.Abstractions;
using KeycloakIntegration.Application.Contracts.Authentication;
using Newtonsoft.Json;

namespace KeycloakIntegration.Application.Core.Authentication;

public sealed record AuthenticationCommand([JsonProperty("client_id")] string ClientId, 
                                           [JsonProperty("client_secret")] string ClientSecret) : ICommand<Result<AuthenticationResponse>>;
