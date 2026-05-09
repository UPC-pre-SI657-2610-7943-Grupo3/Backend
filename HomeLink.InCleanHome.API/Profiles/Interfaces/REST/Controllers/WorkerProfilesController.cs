using System.Net.Mime;
using HomeLink.InCleanHome.API.Profiles.Domain.Model.Commands;
using HomeLink.InCleanHome.API.Profiles.Domain.Model.Queries;
using HomeLink.InCleanHome.API.Profiles.Domain.Services;
using HomeLink.InCleanHome.API.Profiles.Interfaces.REST.Resources;
using HomeLink.InCleanHome.API.Profiles.Interfaces.REST.Transform;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace HomeLink.InCleanHome.API.Profiles.Interfaces.REST.Controllers;

[ApiController]
[Route("api/v1/worker-profiles")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Worker profile endpoints")]
public class WorkerProfilesController(
    IWorkerProfileCommandService commandService,
    IWorkerProfileQueryService queryService) : ControllerBase
{
    [HttpPost]
    [SwaggerOperation("Create Worker Profile", "Create a new worker profile (starts in PENDING state).")]
    public async Task<IActionResult> CreateWorkerProfile([FromBody] CreateWorkerProfileResource resource)
    {
        var command = CreateWorkerProfileCommandFromResourceAssembler.ToCommandFromResource(resource);
        var profile = await commandService.Handle(command);
        if (profile is null) return BadRequest();
        return Ok(WorkerProfileResourceFromEntityAssembler.ToResourceFromEntity(profile));
    }

    [HttpGet("{workerProfileId:int}")]
    [SwaggerOperation("Get Worker Profile By Id", "Get a worker profile by id.")]
    public async Task<IActionResult> GetById(int workerProfileId)
    {
        var profile = await queryService.Handle(new GetWorkerProfileByIdQuery(workerProfileId));
        if (profile is null) return NotFound();
        return Ok(WorkerProfileResourceFromEntityAssembler.ToResourceFromEntity(profile));
    }

    [HttpGet]
    [SwaggerOperation("Get All Worker Profiles", "Get all worker profiles.")]
    public async Task<IActionResult> GetAll()
    {
        var profiles = await queryService.Handle(new GetAllWorkerProfilesQuery());
        var resources = profiles.Select(WorkerProfileResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(resources);
    }

    [HttpPut("{workerProfileId:int}")]
    [SwaggerOperation("Update Worker Profile", "Update biography, experience and hourly rate.")]
    public async Task<IActionResult> Update(int workerProfileId, [FromBody] UpdateWorkerProfileResource resource)
    {
        var command = UpdateWorkerProfileCommandFromResourceAssembler.ToCommandFromResource(workerProfileId, resource);
        var profile = await commandService.Handle(command);
        if (profile is null) return NotFound();
        return Ok(WorkerProfileResourceFromEntityAssembler.ToResourceFromEntity(profile));
    }

    [HttpPut("{workerProfileId:int}/approve")]
    [SwaggerOperation("Approve Worker Profile", "Mark the worker profile as APPROVED (admin only).")]
    public async Task<IActionResult> Approve(int workerProfileId)
    {
        var profile = await commandService.Handle(new ApproveWorkerProfileCommand(workerProfileId));
        if (profile is null) return NotFound();
        return Ok(WorkerProfileResourceFromEntityAssembler.ToResourceFromEntity(profile));
    }
}
