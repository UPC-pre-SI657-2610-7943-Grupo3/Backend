using HomeLink.InCleanHome.API.ReviewsAndEvaluation.Domain.Model.Commands;
using HomeLink.InCleanHome.API.ReviewsAndEvaluation.Interfaces.REST.Resources;

namespace HomeLink.InCleanHome.API.ReviewsAndEvaluation.Interfaces.REST.Transform;

public static class CreateReviewCommandFromResourceAssembler
{
    public static CreateReviewCommand ToCommandFromResource(CreateReviewResource resource)
        => new(resource.BookingRequestId, resource.ClientId, resource.WorkerId, resource.Rating, resource.Comment);
}
