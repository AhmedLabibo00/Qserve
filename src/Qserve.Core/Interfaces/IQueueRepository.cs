using Qserve.Core.Models;

namespace Qserve.Core.Interfaces;

public interface IQueueRepository
{
    int GetNextQueueSequence(int serviceId, DateOnly queueDate);
    QueueTicket AddTicket(QueueTicket ticket);
    QueueTicket? FindTicket(long ticketId);
    void UpdateTicket(QueueTicket ticket);
    int GetWaitingCount(int serviceId);
    int GetCurrentServingSequence(int serviceId);
    bool ServiceExists(int serviceId);
    bool CounterExists(int counterId);
    bool IsCounterMappedToService(int serviceId, int counterId);
    void ResetServiceQueue(int serviceId, DateOnly queueDate);
}
