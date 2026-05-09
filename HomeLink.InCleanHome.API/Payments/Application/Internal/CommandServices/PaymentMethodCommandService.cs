using HomeLink.InCleanHome.API.Payments.Domain.Model.Aggregates;
using HomeLink.InCleanHome.API.Payments.Domain.Model.Commands;
using HomeLink.InCleanHome.API.Payments.Domain.Repositories;
using HomeLink.InCleanHome.API.Payments.Domain.Services;
using HomeLink.InCleanHome.API.Shared.Domain.Repositories;

namespace HomeLink.InCleanHome.API.Payments.Application.Internal.CommandServices;

public class PaymentMethodCommandService(
    IPaymentMethodRepository repository,
    IUnitOfWork unitOfWork) : IPaymentMethodCommandService
{
    public async Task<PaymentMethod?> Handle(RegisterPaymentMethodCommand command)
    {
        var paymentMethod = new PaymentMethod(command);
        await repository.AddAsync(paymentMethod);
        await unitOfWork.CompleteAsync();
        return paymentMethod;
    }
}
