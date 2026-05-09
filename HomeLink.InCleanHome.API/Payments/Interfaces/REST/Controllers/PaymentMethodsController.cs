using System.Net.Mime;
using HomeLink.InCleanHome.API.Payments.Domain.Model.Queries;
using HomeLink.InCleanHome.API.Payments.Domain.Services;
using HomeLink.InCleanHome.API.Payments.Interfaces.REST.Resources;
using HomeLink.InCleanHome.API.Payments.Interfaces.REST.Transform;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace HomeLink.InCleanHome.API.Payments.Interfaces.REST.Controllers;

[ApiController]
[Route("api/v1/payment-methods")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Off-platform payment methods registered by clients")]
public class PaymentMethodsController(
    IPaymentMethodCommandService commandService,
    IPaymentMethodQueryService queryService) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Register([FromBody] RegisterPaymentMethodResource resource)
    {
        var command = RegisterPaymentMethodCommandFromResourceAssembler.ToCommandFromResource(resource);
        var pm = await commandService.Handle(command);
        if (pm is null) return BadRequest();
        return Ok(PaymentMethodResourceFromEntityAssembler.ToResourceFromEntity(pm));
    }

    [HttpGet("by-user/{userId:int}")]
    public async Task<IActionResult> GetByUser(int userId)
    {
        var pms = await queryService.Handle(new GetPaymentMethodsByUserIdQuery(userId));
        return Ok(pms.Select(PaymentMethodResourceFromEntityAssembler.ToResourceFromEntity));
    }
}
