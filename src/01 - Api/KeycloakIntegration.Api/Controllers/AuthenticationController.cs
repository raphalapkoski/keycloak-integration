using AutoMapper;
using FluentResults;
using KeycloakIntegration.Application.Contracts.Authentication;
using KeycloakIntegration.Application.Core.Authentication;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace KeycloakIntegration.Api.Controllers;

[Route("api/authentication")]
[ApiController]
public class AuthenticationController : ControllerBase
{
    readonly IMapper _mapper;
    readonly ISender _sender;

    public AuthenticationController(IMapper mapper, ISender sender)
    {
        _mapper = mapper;
        _sender = sender;
    }

    [HttpPost]
    [Consumes("application/json")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AuthenticationResponse))]
    [ProducesResponseType(typeof(IReadOnlyCollection<Error>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(IReadOnlyCollection<Error>), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> PostAsync([FromBody] AuthenticationRequest request)
    {
        var response = await _sender.Send(_mapper.Map<AuthenticationCommand>(request));
        return Ok(response.Value);
    }
}
