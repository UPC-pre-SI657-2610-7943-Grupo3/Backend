using HomeLink.InCleanHome.API.Payments.Domain.Model.Aggregates;
using HomeLink.InCleanHome.API.Payments.Domain.Model.Queries;
using HomeLink.InCleanHome.API.Payments.Domain.Repositories;
using HomeLink.InCleanHome.API.Payments.Domain.Services;

namespace HomeLink.InCleanHome.API.Payments.Application.Internal.QueryServices;

public class PaymentMethodQueryService(IPaymentMethodRepository repository) : IPaymentMethodQueryService
{
    public async Task<IEnumerable<PaymentMethod>> Handle(GetPaymentMethodsByUserIdQuery query)
        => await repository.ListByUserIdAsync(query.UserId);
}
