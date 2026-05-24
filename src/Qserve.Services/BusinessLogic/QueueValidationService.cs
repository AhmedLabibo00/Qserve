using Qserve.Core.Interfaces;
using Qserve.Core.Models;

namespace Qserve.Services.BusinessLogic;

public sealed class QueueValidationService(IQueueRepository queueRepository)
{
    public void EnsureServiceExists(int serviceId)
    {
        if (!queueRepository.ServiceExists(serviceId))
            throw new InvalidOperationException($"Invalid service id '{serviceId}'.");
    }

    public void EnsureValidTransferState(QueueTicket ticket)
    {
        if (ticket.Status is Core.Enums.TicketStatus.Completed or Core.Enums.TicketStatus.Cancelled)
            throw new InvalidOperationException("Cannot transfer completed or cancelled tickets.");
    }

    public void EnsureValidCounterAssignment(int serviceId, int? counterId)
    {
        if (counterId is null)
            return;

        if (!queueRepository.CounterExists(counterId.Value))
            throw new InvalidOperationException($"Invalid counter id '{counterId.Value}'.");

        if (!queueRepository.IsCounterMappedToService(serviceId, counterId.Value))
            throw new InvalidOperationException("Counter is not mapped to target service.");
    }
}
