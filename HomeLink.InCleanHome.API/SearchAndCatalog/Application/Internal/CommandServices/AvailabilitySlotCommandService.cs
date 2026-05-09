using HomeLink.InCleanHome.API.SearchAndCatalog.Domain.Model.Aggregates;
using HomeLink.InCleanHome.API.SearchAndCatalog.Domain.Model.Commands;
using HomeLink.InCleanHome.API.SearchAndCatalog.Domain.Repositories;
using HomeLink.InCleanHome.API.SearchAndCatalog.Domain.Services;
using HomeLink.InCleanHome.API.Shared.Domain.Repositories;

namespace HomeLink.InCleanHome.API.SearchAndCatalog.Application.Internal.CommandServices;

public class AvailabilitySlotCommandService(
    IAvailabilitySlotRepository repository,
    IUnitOfWork unitOfWork) : IAvailabilitySlotCommandService
{
    public async Task<AvailabilitySlot?> Handle(CreateAvailabilitySlotCommand command)
    {
        var slot = new AvailabilitySlot(command.WorkerProfileId, command.DayOfWeek, command.StartTime, command.EndTime);
        await repository.AddAsync(slot);
        await unitOfWork.CompleteAsync();
        return slot;
    }
}
