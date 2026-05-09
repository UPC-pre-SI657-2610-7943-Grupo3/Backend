using System.Net.Mime;
using HomeLink.InCleanHome.API.Profiles.Domain.Model.Queries;
using HomeLink.InCleanHome.API.Profiles.Domain.Services;
using HomeLink.InCleanHome.API.Profiles.Interfaces.REST.Resources;
using HomeLink.InCleanHome.API.Profiles.Interfaces.REST.Transform;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace HomeLink.InCleanHome.API.Profiles.Interfaces.REST.Controllers;

[ApiController]
[Route("api/v1/client-profiles")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Client profile endpoints")]
public class ClientProfilesController(
    IClientProfileCommandService commandService,
    IClientProfileQueryService queryService) : ControllerBase
{
    [HttpPost]
    [SwaggerOperation("Create Client Profile", "Create a new client profile linked to a user.")]
    public async Task<IActionResult> CreateClientProfile([FromBody] CreateClientProfileResource resource)
    {
        var command = CreateClientProfileCommandFromResourceAssembler.ToCommandFromResource(resource);
        var profile = await commandService.Handle(command);
        if (profile is null) return BadRequest();
        return Ok(ClientProfileResourceFromEntityAssembler.ToResourceFromEntity(profile));
    }

    [HttpGet("by-user/{userId:int}")]
    [SwaggerOperation("Get Client Profile By User Id", "Find a client profile by its associated user id.")]
    public async Task<IActionResult> GetByUserId(int userId)
    {
        var profile = await queryService.Handle(new GetClientProfileByUserIdQuery(userId));
        if (profile is null) return NotFound();
        return Ok(ClientProfileResourceFromEntityAssembler.ToResourceFromEntity(profile));
    }
}
