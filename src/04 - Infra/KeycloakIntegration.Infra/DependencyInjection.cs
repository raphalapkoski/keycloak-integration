using KeycloakIntegration.Domain.External.Keycloak;
using KeycloakIntegration.Infra.External.Keycloak;
using Microsoft.Extensions.DependencyInjection;

namespace KeycloakIntegration.Infra
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfra(this IServiceCollection services)
        {
            services.AddTransient<IKeycloakExternal, KeycloakExternal>();
            return services;
        }
    }
}
