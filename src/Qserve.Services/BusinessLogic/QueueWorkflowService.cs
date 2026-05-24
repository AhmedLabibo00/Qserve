using Qserve.Core.Enums;
using Qserve.Core.Interfaces;
using Qserve.Core.Models;
using Qserve.Printing.Interfaces;

namespace Qserve.Services.BusinessLogic;

public sealed class QueueWorkflowService(
    IQueueRepository queueRepository,
    ITicketPrinter ticketPrinter,
    IDisplayUpdater displayUpdater,
    QueueValidationService validationService)
{
    public QueueTicket GenerateQueue(int serviceId, string servicePrefix, DateOnly queueDate)
    {
        validationService.EnsureServiceExists(serviceId);

        var nextSequence = queueRepository.GetNextQueueSequence(serviceId, queueDate);
        var number = QueueNumberFormatter.Format(servicePrefix, nextSequence);

        var ticket = queueRepository.AddTicket(new QueueTicket
        {
            ServiceId = serviceId,
            QueueDate = queueDate,
            QueueSequence = nextSequence,
            TicketNumber = number,
            Status = TicketStatus.Waiting,
            CreatedAtUtc = DateTime.UtcNow
        });

        ticketPrinter.Print(ticket);
        displayUpdater.UpdateQueueDisplays(ticket);
        displayUpdater.UpdateEmployeeScreen(ticket);

        return ticket;
    }

    public QueueCountersSnapshot GetCurrentAndWaiting(int serviceId, string serviceCode, string serviceNameAr, string serviceNameEn)
    {
        validationService.EnsureServiceExists(serviceId);

        return new QueueCountersSnapshot
        {
            ServiceId = serviceId,
            ServiceCode = serviceCode,
            ServiceNameAr = serviceNameAr,
            ServiceNameEn = serviceNameEn,
            CurrentNumber = queueRepository.GetCurrentServingSequence(serviceId),
            WaitingCount = queueRepository.GetWaitingCount(serviceId)
        };
    }

    public QueueTicket Transfer(TransferRequest request, string targetServicePrefix, DateOnly queueDate)
    {
        var ticket = queueRepository.FindTicket(request.TicketId)
            ?? throw new InvalidOperationException($"Ticket id '{request.TicketId}' not found.");

        validationService.EnsureValidTransferState(ticket);
        validationService.EnsureServiceExists(request.ToServiceId);
        validationService.EnsureValidCounterAssignment(request.ToServiceId, request.ToCounterId);

        ticket.Status = TicketStatus.Transferred;
        ticket.ServiceId = request.ToServiceId;
        ticket.CounterId = request.ToCounterId;

        if (!request.KeepNumber)
        {
            var nextSequence = queueRepository.GetNextQueueSequence(request.ToServiceId, queueDate);
            ticket.QueueSequence = nextSequence;
            ticket.QueueDate = queueDate;
            ticket.TicketNumber = QueueNumberFormatter.Format(targetServicePrefix, nextSequence);
        }

        queueRepository.UpdateTicket(ticket);
        displayUpdater.UpdateQueueDisplays(ticket);
        displayUpdater.UpdateEmployeeScreen(ticket);

        return ticket;
    }

    public void AssignCounter(long ticketId, int serviceId, int counterId)
    {
        validationService.EnsureServiceExists(serviceId);
        validationService.EnsureValidCounterAssignment(serviceId, counterId);

        var ticket = queueRepository.FindTicket(ticketId)
            ?? throw new InvalidOperationException($"Ticket id '{ticketId}' not found.");

        if (ticket.Status is TicketStatus.Completed or TicketStatus.Cancelled)
            throw new InvalidOperationException("Cannot assign counter to completed/cancelled ticket.");

        ticket.CounterId = counterId;
        queueRepository.UpdateTicket(ticket);
        displayUpdater.UpdateEmployeeScreen(ticket);
    }

    public void ResetQueue(int serviceId, DateOnly queueDate)
    {
        validationService.EnsureServiceExists(serviceId);
        queueRepository.ResetServiceQueue(serviceId, queueDate);
    }
}
