using JobApplication.Application.Notifications;
using MediatR;
using Microsoft.Extensions.Logging;

namespace JobApplication.Application.Notifications.Handlers;

/// <summary>
/// Handler 1 of N for <see cref="ApplicationsRejectedNotification"/>.
/// Writes a structured audit log entry for every auto-rejected application.
/// </summary>
public class ApplicationsRejectedAuditHandler : INotificationHandler<ApplicationsRejectedNotification>
{
    private readonly ILogger<ApplicationsRejectedAuditHandler> _logger;

    public ApplicationsRejectedAuditHandler(ILogger<ApplicationsRejectedAuditHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(ApplicationsRejectedNotification notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "[AUDIT] {Count} application(s) auto-rejected at {RejectedAt} " +
            "after sitting untouched for {Days} days. Application IDs: [{Ids}]",
            notification.RejectedApplicationIds.Count,
            notification.RejectedAt,
            notification.StaleAfterDays,
            string.Join(", ", notification.RejectedApplicationIds));

        // TODO: persist to an AuditLog table.

        return Task.CompletedTask;
    }
}
