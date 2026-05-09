using System.Net.Mime;
using HomeLink.InCleanHome.API.IAM.Domain.Model.Commands;
using HomeLink.InCleanHome.API.IAM.Domain.Model.Queries;
using HomeLink.InCleanHome.API.IAM.Domain.Services;
using HomeLink.InCleanHome.API.IAM.Interfaces.REST.Resources;
using HomeLink.InCleanHome.API.IAM.Interfaces.REST.Transform;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace HomeLink.InCleanHome.API.IAM.Interfaces.REST.Controllers;

[ApiController]
[Route("api/v1/users")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("User account read-only endpoints (admin only)")]
public class UsersController(
    IUserCommandService userCommandService,
    IUserQueryService userQueryService) : ControllerBase
{
    [HttpGet]
    [SwaggerOperation("Get All Users", "Get all users in the system.")]
    public async Task<IActionResult> GetAllUsers()
    {
        var users = await userQueryService.Handle(new GetAllUsersQuery());
        var resources = users.Select(UserResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(resources);
    }

    [HttpGet("{userId:int}")]
    [SwaggerOperation("Get User By Id", "Get a user by id.")]
    public async Task<IActionResult> GetUserById(int userId)
    {
        var user = await userQueryService.Handle(new GetUserByIdQuery(userId));
        if (user is null) return NotFound();
        return Ok(UserResourceFromEntityAssembler.ToResourceFromEntity(user));
    }

    [HttpPut("{userId:int}/verify")]
    [SwaggerOperation("Verify User", "Activate a worker account after admin review.")]
    public async Task<IActionResult> VerifyUser(int userId)
    {
        await userCommandService.Handle(new VerifyUserCommand(userId));
        return Ok(new { message = $"User {userId} successfully verified." });
    }
}
