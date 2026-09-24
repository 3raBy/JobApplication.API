using JobApplication.Application.Notifications;
using MediatR;
using Microsoft.Extensions.Logging;

namespace JobApplication.Application.Notifications.Handlers;

/// <summary>
/// Handler 2 of N for <see cref="JobsClosedNotification"/>.
/// Simulates sending notification emails to applicants of the closed jobs.
/// Replace the TODO block with a real IEmailService call when ready.
/// </summary>
public class JobsClosedEmailHandler : INotificationHandler<JobsClosedNotification>
{
    private readonly ILogger<JobsClosedEmailHandler> _logger;

    public JobsClosedEmailHandler(ILogger<JobsClosedEmailHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(JobsClosedNotification notification, CancellationToken cancellationToken)
    {
        // TODO: inject IEmailService and send real emails to each job's applicants.
        // Example:
        //   foreach (var jobId in notification.ClosedJobIds)
        //       await _emailService.NotifyApplicantsJobClosedAsync(jobId, cancellationToken);

        _logger.LogInformation(
            "[EMAIL] Would notify applicants for {Count} closed job(s): [{Ids}]",
            notification.ClosedJobIds.Count,
            string.Join(", ", notification.ClosedJobIds));

        return Task.CompletedTask;
    }
}
