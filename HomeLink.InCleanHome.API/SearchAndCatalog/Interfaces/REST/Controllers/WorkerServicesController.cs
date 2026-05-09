using System.Net.Mime;
using HomeLink.InCleanHome.API.SearchAndCatalog.Domain.Model.Queries;
using HomeLink.InCleanHome.API.SearchAndCatalog.Domain.Services;
using HomeLink.InCleanHome.API.SearchAndCatalog.Interfaces.REST.Resources;
using HomeLink.InCleanHome.API.SearchAndCatalog.Interfaces.REST.Transform;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace HomeLink.InCleanHome.API.SearchAndCatalog.Interfaces.REST.Controllers;

[ApiController]
[Route("api/v1/worker-services")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Worker services catalog endpoints")]
public class WorkerServicesController(
    IWorkerServiceCommandService commandService,
    IWorkerServiceQueryService queryService) : ControllerBase
{
    [HttpPost]
    [SwaggerOperation("Create Worker Service", "Publish a new service offered by a worker.")]
    public async Task<IActionResult> Create([FromBody] CreateWorkerServiceResource resource)
    {
        var command = CreateWorkerServiceCommandFromResourceAssembler.ToCommandFromResource(resource);
        var service = await commandService.Handle(command);
        if (service is null) return BadRequest();
        return Ok(WorkerServiceResourceFromEntityAssembler.ToResourceFromEntity(service));
    }

    [HttpGet("by-worker/{workerProfileId:int}")]
    public async Task<IActionResult> GetByWorker(int workerProfileId)
    {
        var services = await queryService.Handle(new GetWorkerServicesByWorkerIdQuery(workerProfileId));
        return Ok(services.Select(WorkerServiceResourceFromEntityAssembler.ToResourceFromEntity));
    }

    [HttpGet("search")]
    [SwaggerOperation("Search", "Search worker services by category, district, max price or day of the week.")]
    public async Task<IActionResult> Search(
        [FromQuery] int? categoryId,
        [FromQuery] string? district,
        [FromQuery] decimal? maxPrice,
        [FromQuery] int? dayOfWeek)
    {
        var services = await queryService.Handle(new SearchWorkerServicesQuery(categoryId, district, maxPrice, dayOfWeek));
        return Ok(services.Select(WorkerServiceResourceFromEntityAssembler.ToResourceFromEntity));
    }
}
