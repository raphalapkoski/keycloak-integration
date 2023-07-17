using KeycloakIntegration.Application;
using KeycloakIntegration.Application.Middleware;
using KeycloakIntegration.Infra;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

builder.Services.AddApplication();

builder.Services.AddInfra();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, o =>
                {
                    o.MetadataAddress = builder.Configuration.GetValue<string>("Keycloak:metadata-address-url");
                    o.Authority = builder.Configuration.GetValue<string>("Keycloak:authority-server-url");
                    o.RequireHttpsMetadata = builder.Configuration.GetValue<bool>("Keycloak:require-https-metadata");

                    o.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateAudience = builder.Configuration.GetValue<bool>("Keycloak:validate-token-audience"),
                        ValidateLifetime = builder.Configuration.GetValue<bool>("Keycloak:validate-token-lifetime"),
                        ValidAudience = builder.Configuration.GetValue<string>("Keycloak:audience")
                    };
                });

builder.Services.AddHttpClient("keycloak", client =>
{
    client.BaseAddress = new Uri(builder.Configuration.GetValue<string>("Keycloak:base-url"));
});

var app = builder.Build();

app.UseMiddleware<ExceptionHandlingMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
