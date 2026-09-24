using JobApplication.Application.Notifications;
using MediatR;
using Microsoft.Extensions.Logging;

namespace JobApplication.Application.Notifications.Handlers;

/// <summary>
/// Handler 1 of N for <see cref="JobsClosedNotification"/>.
/// Writes a structured audit log entry for every closed job.
/// </summary>
public class JobsClosedAuditHandler : INotificationHandler<JobsClosedNotification>
{
    private readonly ILogger<JobsClosedAuditHandler> _logger;

    public JobsClosedAuditHandler(ILogger<JobsClosedAuditHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(JobsClosedNotification notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "[AUDIT] {Count} job(s) closed at {ClosedAt} by '{ClosedBy}'. Job IDs: [{Ids}]",
            notification.ClosedJobIds.Count,
            notification.ClosedAt,
            notification.ClosedBy,
            string.Join(", ", notification.ClosedJobIds));

        // TODO: persist to an AuditLog table, send emails, push a SignalR event, etc.

        return Task.CompletedTask;
    }
}
