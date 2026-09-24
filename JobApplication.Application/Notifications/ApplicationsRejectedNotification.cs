using MediatR;

namespace JobApplication.Application.Notifications;

/// <summary>
/// Published by the <c>AutoRejectStaleApplications</c> recurring job after it
/// automatically rejects applications that have been untouched for too long.
/// Any number of <see cref="INotificationHandler{TNotification}"/> implementations
/// can react to this event independently (e.g. notify candidates, write audit logs).
/// </summary>
public class ApplicationsRejectedNotification : INotification
{
    /// <summary>IDs of every application that was just auto-rejected.</summary>
    public IReadOnlyList<int> RejectedApplicationIds { get; init; } = [];

    /// <summary>UTC timestamp when the rejection happened.</summary>
    public DateTime RejectedAt { get; init; } = DateTime.UtcNow;

    /// <summary>
    /// Number of days an application sat untouched before being auto-rejected.
    /// </summary>
    public int StaleAfterDays { get; init; }
}
