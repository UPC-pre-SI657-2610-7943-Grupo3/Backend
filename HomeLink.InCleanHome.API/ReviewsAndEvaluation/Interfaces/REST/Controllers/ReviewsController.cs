using System.Net.Mime;
using HomeLink.InCleanHome.API.ReviewsAndEvaluation.Domain.Model.Queries;
using HomeLink.InCleanHome.API.ReviewsAndEvaluation.Domain.Services;
using HomeLink.InCleanHome.API.ReviewsAndEvaluation.Interfaces.REST.Resources;
using HomeLink.InCleanHome.API.ReviewsAndEvaluation.Interfaces.REST.Transform;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace HomeLink.InCleanHome.API.ReviewsAndEvaluation.Interfaces.REST.Controllers;

[ApiController]
[Route("api/v1/reviews")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Service review and rating endpoints")]
public class ReviewsController(
    IReviewCommandService commandService,
    IReviewQueryService queryService) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateReviewResource resource)
    {
        var command = CreateReviewCommandFromResourceAssembler.ToCommandFromResource(resource);
        var review = await commandService.Handle(command);
        if (review is null) return BadRequest("Review for this booking already exists.");
        return Ok(ReviewResourceFromEntityAssembler.ToResourceFromEntity(review));
    }

    [HttpGet("by-worker/{workerId:int}")]
    public async Task<IActionResult> GetByWorker(int workerId)
    {
        var reviews = await queryService.Handle(new GetReviewsByWorkerIdQuery(workerId));
        return Ok(reviews.Select(ReviewResourceFromEntityAssembler.ToResourceFromEntity));
    }

    [HttpGet("by-client/{clientId:int}")]
    public async Task<IActionResult> GetByClient(int clientId)
    {
        var reviews = await queryService.Handle(new GetReviewsByClientIdQuery(clientId));
        return Ok(reviews.Select(ReviewResourceFromEntityAssembler.ToResourceFromEntity));
    }
}
