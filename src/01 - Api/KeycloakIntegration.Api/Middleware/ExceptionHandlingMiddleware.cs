using KeycloakIntegration.Api.Common;
using KeycloakIntegration.Api.Constants;
using KeycloakIntegration.Application.Contracts.Error;
using Newtonsoft.Json;
using System.Net;

namespace KeycloakIntegration.Application.Middleware;

internal sealed class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Exception occurred: {Message}", ex.Message);

            await HandleExceptionAsync(httpContext, ex);
        }
    }

    private static async Task HandleExceptionAsync(HttpContext httpContext, Exception exception)
    {
        (HttpStatusCode httpStatusCode, IReadOnlyCollection<CustomError> errors) = GetHttpStatusCodeAndErrors(exception);

        httpContext.Response.ContentType = "application/json";

        httpContext.Response.StatusCode = (int)httpStatusCode;

        var serializerOptions = new JsonSerializerSettings
        {
            NullValueHandling = NullValueHandling.Ignore,
        };

        string response = JsonConvert.SerializeObject(new ApiErrorResponse(errors), serializerOptions);

        await httpContext.Response.WriteAsync(response);
    }

    private static (HttpStatusCode HttpStatusCode, IReadOnlyCollection<CustomError> Errors) GetHttpStatusCodeAndErrors(Exception exception) =>
        exception switch
        {
            FluentValidation.ValidationException validationException => (HttpStatusCode.BadRequest, validationException.Errors.Select(_ => new CustomError(_.ErrorCode, _.ErrorMessage)).ToList()),
            UnauthorizedAccessException unauthorizedAccessException => (HttpStatusCode.Unauthorized, new[] { new CustomError("Unauthorized", exception.Message) }),
            _ => (HttpStatusCode.InternalServerError, new[] { Errors.ServerError })
        };
}
