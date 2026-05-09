using HomeLink.InCleanHome.API.Payments.Domain.Model.Aggregates;
using HomeLink.InCleanHome.API.Payments.Domain.Model.Commands;

namespace HomeLink.InCleanHome.API.Payments.Domain.Services;

public interface IPaymentMethodCommandService
{
    Task<PaymentMethod?> Handle(RegisterPaymentMethodCommand command);
}
