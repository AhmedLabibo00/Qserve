using Qserve.Core.Models;

namespace Qserve.Core.Interfaces;

public interface IDisplayUpdater
{
    void UpdateQueueDisplays(QueueTicket ticket);
    void UpdateEmployeeScreen(QueueTicket ticket);
    void UpdateServiceSnapshot(QueueCountersSnapshot snapshot);
}
