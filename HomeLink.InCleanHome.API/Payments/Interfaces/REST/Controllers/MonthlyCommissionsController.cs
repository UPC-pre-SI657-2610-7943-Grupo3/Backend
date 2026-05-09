using System.Net.Mime;
using HomeLink.InCleanHome.API.Payments.Domain.Model.Commands;
using HomeLink.InCleanHome.API.Payments.Domain.Model.Queries;
using HomeLink.InCleanHome.API.Payments.Domain.Services;
using HomeLink.InCleanHome.API.Payments.Interfaces.REST.Resources;
using HomeLink.InCleanHome.API.Payments.Interfaces.REST.Transform;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace HomeLink.InCleanHome.API.Payments.Interfaces.REST.Controllers;

[ApiController]
[Route("api/v1/monthly-commissions")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Monthly commission endpoints (10% on worker earnings)")]
public class MonthlyCommissionsController(
    IMonthlyCommissionCommandService commandService,
    IMonthlyCommissionQueryService queryService) : ControllerBase
{
    [HttpPost("calculate")]
    [SwaggerOperation("Calculate Commission",
        "Calculate the monthly commission (10% of total earnings) for a worker in a given period.")]
    public async Task<IActionResult> Calculate([FromBody] CalculateMonthlyCommissionResource resource)
    {
        var command = CalculateMonthlyCommissionCommandFromResourceAssembler.ToCommandFromResource(resource);
        var commission = await commandService.Handle(command);
        if (commission is null) return BadRequest();
        return Ok(MonthlyCommissionResourceFromEntityAssembler.ToResourceFromEntity(commission));
    }

    [HttpGet("by-worker/{workerId:int}")]
    public async Task<IActionResult> GetByWorker(int workerId)
    {
        var commissions = await queryService.Handle(new GetCommissionsByWorkerIdQuery(workerId));
        return Ok(commissions.Select(MonthlyCommissionResourceFromEntityAssembler.ToResourceFromEntity));
    }

    [HttpGet("by-worker/{workerId:int}/period/{year:int}/{month:int}")]
    public async Task<IActionResult> GetByPeriod(int workerId, int year, int month)
    {
        var commission = await queryService.Handle(new GetCommissionByPeriodQuery(workerId, year, month));
        if (commission is null) return NotFound();
        return Ok(MonthlyCommissionResourceFromEntityAssembler.ToResourceFromEntity(commission));
    }

    [HttpPut("{commissionId:int}/mark-paid")]
    public async Task<IActionResult> MarkPaid(int commissionId)
    {
        var commission = await commandService.Handle(new MarkCommissionPaidCommand(commissionId));
        if (commission is null) return NotFound();
        return Ok(MonthlyCommissionResourceFromEntityAssembler.ToResourceFromEntity(commission));
    }
}
