using System.Net.Mime;
using HomeLink.InCleanHome.API.Booking.Domain.Model.Commands;
using HomeLink.InCleanHome.API.Booking.Domain.Model.Queries;
using HomeLink.InCleanHome.API.Booking.Domain.Services;
using HomeLink.InCleanHome.API.Booking.Interfaces.REST.Resources;
using HomeLink.InCleanHome.API.Booking.Interfaces.REST.Transform;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace HomeLink.InCleanHome.API.Booking.Interfaces.REST.Controllers;

[ApiController]
[Route("api/v1/booking-requests")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Booking request endpoints — handle the entire hiring lifecycle")]
public class BookingRequestsController(
    IBookingRequestCommandService commandService,
    IBookingRequestQueryService queryService) : ControllerBase
{
    [HttpPost]
    [SwaggerOperation("Create Booking Request", "A client creates a new hiring request to a worker.")]
    public async Task<IActionResult> Create([FromBody] CreateBookingRequestResource resource)
    {
        var command = CreateBookingRequestCommandFromResourceAssembler.ToCommandFromResource(resource);
        var booking = await commandService.Handle(command);
        if (booking is null) return BadRequest();
        return Ok(BookingRequestResourceFromEntityAssembler.ToResourceFromEntity(booking));
    }

    [HttpGet("{bookingId:int}")]
    public async Task<IActionResult> GetById(int bookingId)
    {
        var booking = await queryService.Handle(new GetBookingByIdQuery(bookingId));
        if (booking is null) return NotFound();
        return Ok(BookingRequestResourceFromEntityAssembler.ToResourceFromEntity(booking));
    }

    [HttpGet("by-client/{clientId:int}")]
    public async Task<IActionResult> GetByClient(int clientId)
    {
        var bookings = await queryService.Handle(new GetBookingsByClientQuery(clientId));
        return Ok(bookings.Select(BookingRequestResourceFromEntityAssembler.ToResourceFromEntity));
    }

    [HttpGet("by-worker/{workerId:int}")]
    public async Task<IActionResult> GetByWorker(int workerId)
    {
        var bookings = await queryService.Handle(new GetBookingsByWorkerQuery(workerId));
        return Ok(bookings.Select(BookingRequestResourceFromEntityAssembler.ToResourceFromEntity));
    }

    [HttpPut("{bookingId:int}/accept")]
    public async Task<IActionResult> Accept(int bookingId)
    {
        var booking = await commandService.Handle(new AcceptBookingCommand(bookingId));
        if (booking is null) return NotFound();
        return Ok(BookingRequestResourceFromEntityAssembler.ToResourceFromEntity(booking));
    }

    [HttpPut("{bookingId:int}/reject")]
    public async Task<IActionResult> Reject(int bookingId)
    {
        var booking = await commandService.Handle(new RejectBookingCommand(bookingId));
        if (booking is null) return NotFound();
        return Ok(BookingRequestResourceFromEntityAssembler.ToResourceFromEntity(booking));
    }

    [HttpPut("{bookingId:int}/reschedule")]
    public async Task<IActionResult> Reschedule(int bookingId, [FromBody] RescheduleBookingResource resource)
    {
        var booking = await commandService.Handle(new RescheduleBookingCommand(bookingId, resource.NewScheduledAt));
        if (booking is null) return NotFound();
        return Ok(BookingRequestResourceFromEntityAssembler.ToResourceFromEntity(booking));
    }

    [HttpPut("{bookingId:int}/cancel")]
    public async Task<IActionResult> Cancel(int bookingId, [FromBody] CancelBookingResource resource)
    {
        var booking = await commandService.Handle(new CancelBookingCommand(bookingId, resource.CancelledBy));
        if (booking is null) return NotFound();
        return Ok(BookingRequestResourceFromEntityAssembler.ToResourceFromEntity(booking));
    }

    [HttpPut("{bookingId:int}/complete")]
    public async Task<IActionResult> Complete(int bookingId)
    {
        var booking = await commandService.Handle(new CompleteBookingCommand(bookingId));
        if (booking is null) return NotFound();
        return Ok(BookingRequestResourceFromEntityAssembler.ToResourceFromEntity(booking));
    }
}
