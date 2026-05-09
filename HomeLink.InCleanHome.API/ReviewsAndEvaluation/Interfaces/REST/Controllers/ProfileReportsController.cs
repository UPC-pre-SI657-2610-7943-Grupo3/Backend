using System.Net.Mime;
using HomeLink.InCleanHome.API.ReviewsAndEvaluation.Domain.Model.Queries;
using HomeLink.InCleanHome.API.ReviewsAndEvaluation.Domain.Services;
using HomeLink.InCleanHome.API.ReviewsAndEvaluation.Interfaces.REST.Resources;
using HomeLink.InCleanHome.API.ReviewsAndEvaluation.Interfaces.REST.Transform;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace HomeLink.InCleanHome.API.ReviewsAndEvaluation.Interfaces.REST.Controllers;

[ApiController]
[Route("api/v1/profile-reports")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Endpoints for users to report suspicious profiles")]
public class ProfileReportsController(
    IProfileReportCommandService commandService,
    IProfileReportQueryService queryService) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateProfileReportResource resource)
    {
        var command = CreateProfileReportCommandFromResourceAssembler.ToCommandFromResource(resource);
        var report = await commandService.Handle(command);
        if (report is null) return BadRequest();
        return Ok(ProfileReportResourceFromEntityAssembler.ToResourceFromEntity(report));
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var reports = await queryService.Handle(new GetProfileReportsQuery());
        return Ok(reports.Select(ProfileReportResourceFromEntityAssembler.ToResourceFromEntity));
    }
}
