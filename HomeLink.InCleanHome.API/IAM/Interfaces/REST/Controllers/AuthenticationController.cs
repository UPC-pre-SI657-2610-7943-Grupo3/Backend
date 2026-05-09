using System.Net.Mime;
using HomeLink.InCleanHome.API.IAM.Domain.Services;
using HomeLink.InCleanHome.API.IAM.Infrastructure.Pipeline.Middleware.Attributes;
using HomeLink.InCleanHome.API.IAM.Interfaces.REST.Resources;
using HomeLink.InCleanHome.API.IAM.Interfaces.REST.Transform;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace HomeLink.InCleanHome.API.IAM.Interfaces.REST.Controllers;

/// <summary>
///     Authentication endpoints (sign-up, sign-in) for clients and workers.
/// </summary>
[ApiController]
[Route("api/v1/authentication")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Available authentication endpoints (sign-up & sign-in)")]
public class AuthenticationController(IUserCommandService userCommandService) : ControllerBase
{
    [HttpPost("sign-up")]
    [AllowAnonymous]
    [SwaggerOperation("Sign Up", "Register a new client or worker account.", OperationId = "SignUp")]
    [SwaggerResponse(200, "The user has been successfully registered.")]
    [SwaggerResponse(400, "The user could not be registered.")]
    public async Task<IActionResult> SignUp([FromBody] SignUpResource resource)
    {
        var command = SignUpCommandFromResourceAssembler.ToCommandFromResource(resource);
        await userCommandService.Handle(command);
        return Ok(new { message = "User successfully registered." });
    }

    [HttpPost("sign-in")]
    [AllowAnonymous]
    [SwaggerOperation("Sign In", "Authenticate a user and obtain a JWT token.", OperationId = "SignIn")]
    [SwaggerResponse(200, "The user has been authenticated.", typeof(AuthenticatedUserResource))]
    [SwaggerResponse(401, "Invalid credentials.")]
    public async Task<IActionResult> SignIn([FromBody] SignInResource resource)
    {
        var command = SignInCommandFromResourceAssembler.ToCommandFromResource(resource);
        var (user, token) = await userCommandService.Handle(command);
        var authenticatedUser = AuthenticatedUserResourceFromEntityAssembler.ToResourceFromEntity(user, token);
        return Ok(authenticatedUser);
    }
}
