using System.Net.Mime;
using HomeLink.InCleanHome.API.SearchAndCatalog.Domain.Model.Queries;
using HomeLink.InCleanHome.API.SearchAndCatalog.Domain.Services;
using HomeLink.InCleanHome.API.SearchAndCatalog.Interfaces.REST.Resources;
using HomeLink.InCleanHome.API.SearchAndCatalog.Interfaces.REST.Transform;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace HomeLink.InCleanHome.API.SearchAndCatalog.Interfaces.REST.Controllers;

[ApiController]
[Route("api/v1/service-categories")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Service category endpoints")]
public class ServiceCategoriesController(
    IServiceCategoryCommandService commandService,
    IServiceCategoryQueryService queryService) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateServiceCategoryResource resource)
    {
        var command = CreateServiceCategoryCommandFromResourceAssembler.ToCommandFromResource(resource);
        var category = await commandService.Handle(command);
        if (category is null) return BadRequest("Service category already exists.");
        return Ok(ServiceCategoryResourceFromEntityAssembler.ToResourceFromEntity(category));
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var categories = await queryService.Handle(new GetAllServiceCategoriesQuery());
        return Ok(categories.Select(ServiceCategoryResourceFromEntityAssembler.ToResourceFromEntity));
    }
}
