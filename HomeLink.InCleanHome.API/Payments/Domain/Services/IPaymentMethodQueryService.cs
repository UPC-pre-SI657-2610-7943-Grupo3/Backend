using HomeLink.InCleanHome.API.Payments.Domain.Model.Aggregates;
using HomeLink.InCleanHome.API.Payments.Domain.Model.Queries;

namespace HomeLink.InCleanHome.API.Payments.Domain.Services;

public interface IPaymentMethodQueryService
{
    Task<IEnumerable<PaymentMethod>> Handle(GetPaymentMethodsByUserIdQuery query);
}
