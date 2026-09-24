using JobApplication.Application.Notifications;
using MediatR;
using Microsoft.Extensions.Logging;

namespace JobApplication.Application.Notifications.Handlers;

/// <summary>
/// Handler 2 of N for <see cref="ApplicationsRejectedNotification"/>.
/// Simulates notifying each rejected candidate via email.
/// Replace the TODO block with a real IEmailService call when ready.
/// </summary>
public class ApplicationsRejectedEmailHandler : INotificationHandler<ApplicationsRejectedNotification>
{
    private readonly ILogger<ApplicationsRejectedEmailHandler> _logger;

    public ApplicationsRejectedEmailHandler(ILogger<ApplicationsRejectedEmailHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(ApplicationsRejectedNotification notification, CancellationToken cancellationToken)
    {
        // TODO: inject IEmailService and send real rejection emails.
        // Example:
        //   foreach (var appId in notification.RejectedApplicationIds)
        //       await _emailService.SendRejectionEmailAsync(appId, cancellationToken);

        _logger.LogInformation(
            "[EMAIL] Would send rejection emails for {Count} application(s): [{Ids}]",
            notification.RejectedApplicationIds.Count,
            string.Join(", ", notification.RejectedApplicationIds));

        return Task.CompletedTask;
    }
}
