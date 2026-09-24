using MediatR;

namespace JobApplication.Application.Notifications;

/// <summary>
/// Published by the <c>CloseExpiredJobs</c> recurring job after it
/// successfully closes one or more expired job postings.
/// Any number of <see cref="INotificationHandler{TNotification}"/> implementations
/// can react to this event independently (e.g. send emails, write audit logs).
/// </summary>
public class JobsClosedNotification : INotification
{
    /// <summary>IDs of every job that was just closed.</summary>
    public IReadOnlyList<int> ClosedJobIds { get; init; } = [];

    /// <summary>UTC timestamp when the closing happened.</summary>
    public DateTime ClosedAt { get; init; } = DateTime.UtcNow;

    /// <summary>Who / what triggered the closure.</summary>
    public string ClosedBy { get; init; } = "System (Recurring Job)";
}
