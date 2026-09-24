namespace JobApplication.Application.Interfaces;

/// <summary>
/// Defines recurring background job operations for the Job Application system.
/// </summary>
public interface IRecurringJobService
{
    /// <summary>
    /// Closes all active jobs whose deadline has passed.
    /// Runs daily at midnight.
    /// </summary>
    Task CloseExpiredJobsAsync();

    /// <summary>
    /// Automatically rejects applications that have been pending
    /// for more than 30 days with no status update.
    /// Runs every 6 hours.
    /// </summary>
    Task AutoRejectStaleApplicationsAsync();
}
