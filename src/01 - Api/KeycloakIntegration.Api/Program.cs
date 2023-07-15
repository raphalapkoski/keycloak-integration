using KeycloakIntegration.Application;
using KeycloakIntegration.Application.Middleware;
using KeycloakIntegration.Infra;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

builder.Services.AddApplication();

builder.Services.AddInfra();

builder.Services.AddHttpClient("keycloak", client =>
{
    client.BaseAddress = new Uri(builder.Configuration.GetValue<string>("Keycloak:authentication-url"));
});

var app = builder.Build();

app.UseMiddleware<ExceptionHandlingMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
