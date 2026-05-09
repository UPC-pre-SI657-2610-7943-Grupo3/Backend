using HomeLink.InCleanHome.API.ReviewsAndEvaluation.Domain.Model.Aggregates;
using HomeLink.InCleanHome.API.ReviewsAndEvaluation.Interfaces.REST.Resources;

namespace HomeLink.InCleanHome.API.ReviewsAndEvaluation.Interfaces.REST.Transform;

public static class ReviewResourceFromEntityAssembler
{
    public static ReviewResource ToResourceFromEntity(Review entity)
        => new(entity.Id, entity.BookingRequestId, entity.ClientId, entity.WorkerId, entity.Rating, entity.Comment);
}
