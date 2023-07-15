using AutoMapper;
using KeycloakIntegration.Application.Contracts.Authentication;
using KeycloakIntegration.Application.Core.Authentication;

namespace KeycloakIntegration.Application.Mappers;

internal class AuthenticationMapping : Profile
{
    public AuthenticationMapping()
    {
        CreateMap<AuthenticationRequest, AuthenticationCommand>();
    }
}
