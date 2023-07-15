using FluentValidation;

namespace KeycloakIntegration.Application.Core.Authentication
{
    public sealed class AuthenticationCommandValidator : AbstractValidator<AuthenticationCommand>
    {
        public AuthenticationCommandValidator()
        {
            RuleFor(_ => _.ClientId)
                .NotEmpty()
                .WithMessage("ClientId deve ser informado");

            RuleFor(_ => _.ClientSecret)
                .NotEmpty()
                .WithMessage("ClientSecret deve ser informada");
        }
    }
}
