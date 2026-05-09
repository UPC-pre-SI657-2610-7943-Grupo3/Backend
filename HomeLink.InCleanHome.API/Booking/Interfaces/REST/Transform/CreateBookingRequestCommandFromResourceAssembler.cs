using HomeLink.InCleanHome.API.Booking.Domain.Model.Commands;
using HomeLink.InCleanHome.API.Booking.Interfaces.REST.Resources;

namespace HomeLink.InCleanHome.API.Booking.Interfaces.REST.Transform;

public static class CreateBookingRequestCommandFromResourceAssembler
{
    public static CreateBookingRequestCommand ToCommandFromResource(CreateBookingRequestResource resource)
        => new(resource.ClientId, resource.WorkerId, resource.WorkerServiceId,
               resource.ScheduledAt, resource.AgreedPrice, resource.Notes);
}
