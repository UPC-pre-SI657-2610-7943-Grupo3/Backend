using HomeLink.InCleanHome.API.Booking.Domain.Model.Aggregates;
using HomeLink.InCleanHome.API.Booking.Interfaces.REST.Resources;

namespace HomeLink.InCleanHome.API.Booking.Interfaces.REST.Transform;

public static class BookingRequestResourceFromEntityAssembler
{
    public static BookingRequestResource ToResourceFromEntity(BookingRequest entity)
        => new(entity.Id, entity.ClientId, entity.WorkerId, entity.WorkerServiceId,
               entity.ScheduledAt, entity.Status, entity.AgreedPrice, entity.Notes);
}
