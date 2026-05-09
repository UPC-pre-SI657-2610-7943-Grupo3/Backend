using System.Net.Mime;
using HomeLink.InCleanHome.API.SearchAndCatalog.Domain.Model.Queries;
using HomeLink.InCleanHome.API.SearchAndCatalog.Domain.Services;
using HomeLink.InCleanHome.API.SearchAndCatalog.Interfaces.REST.Resources;
using HomeLink.InCleanHome.API.SearchAndCatalog.Interfaces.REST.Transform;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace HomeLink.InCleanHome.API.SearchAndCatalog.Interfaces.REST.Controllers;

[ApiController]
[Route("api/v1/availability-slots")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Worker weekly availability endpoints")]
public class AvailabilitySlotsController(
    IAvailabilitySlotCommandService commandService,
    IAvailabilitySlotQueryService queryService) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateAvailabilitySlotResource resource)
    {
        var command = CreateAvailabilitySlotCommandFromResourceAssembler.ToCommandFromResource(resource);
        var slot = await commandService.Handle(command);
        if (slot is null) return BadRequest();
        return Ok(AvailabilitySlotResourceFromEntityAssembler.ToResourceFromEntity(slot));
    }

    [HttpGet("by-worker/{workerProfileId:int}")]
    public async Task<IActionResult> GetByWorker(int workerProfileId)
    {
        var slots = await queryService.Handle(new GetAvailabilityByWorkerIdQuery(workerProfileId));
        return Ok(slots.Select(AvailabilitySlotResourceFromEntityAssembler.ToResourceFromEntity));
    }
}
