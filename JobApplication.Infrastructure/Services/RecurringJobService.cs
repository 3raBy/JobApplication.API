using JobApplication.Application.Interfaces;
using JobApplication.Domain.Enums;
using Microsoft.Extensions.Logging;

namespace JobApplication.Infrastructure.Services;

/// <summary>
/// Implements recurring background jobs for the Job Application system using Hangfire.
/// </summary>
public class RecurringJobService : IRecurringJobService
{
    private readonly IJobRepository _jobRepository;
    private readonly IApplicationRepository _applicationRepository;
    private readonly ILogger<RecurringJobService> _logger;

    // How many days a Pending application can remain untouched before auto-rejection.
    private const int StaleApplicationDays = 30;

    public RecurringJobService(
        IJobRepository jobRepository,
        IApplicationRepository applicationRepository,
        ILogger<RecurringJobService> logger)
    {
        _jobRepository = jobRepository;
        _applicationRepository = applicationRepository;
        _logger = logger;
    }

    /// <inheritdoc/>
    public async Task CloseExpiredJobsAsync()
    {
        _logger.LogInformation("[RecurringJob] CloseExpiredJobs started at {Time}", DateTime.UtcNow);

        var expiredJobs = (await _jobRepository.GetActiveExpiredJobsAsync()).ToList();

        if (expiredJobs.Count == 0)
        {
            _logger.LogInformation("[RecurringJob] CloseExpiredJobs — no expired jobs found.");
            return;
        }

        await _jobRepository.CloseJobsAsync(expiredJobs, closedBy: "System (Recurring Job)");

        _logger.LogInformation(
            "[RecurringJob] CloseExpiredJobs — closed {Count} job(s): [{Ids}]",
            expiredJobs.Count,
            string.Join(", ", expiredJobs.Select(j => j.Id)));
    }

    /// <inheritdoc/>
    public async Task AutoRejectStaleApplicationsAsync()
    {
        _logger.LogInformation("[RecurringJob] AutoRejectStaleApplications started at {Time}", DateTime.UtcNow);

        var staleApps = (await _applicationRepository.GetStalePendingApplicationsAsync(StaleApplicationDays)).ToList();

        if (staleApps.Count == 0)
        {
            _logger.LogInformation("[RecurringJob] AutoRejectStaleApplications — no stale applications found.");
            return;
        }

        var now = DateTime.UtcNow;
        foreach (var app in staleApps)
        {
            app.ApplicationStatus = ApplicationStatus.Rejected;
            app.StatusUpdatedAt = now;
        }

        await _applicationRepository.BulkUpdateApplicationsAsync(staleApps);

        _logger.LogInformation(
            "[RecurringJob] AutoRejectStaleApplications — rejected {Count} stale application(s): [{Ids}]",
            staleApps.Count,
            string.Join(", ", staleApps.Select(a => a.Id)));
    }
}
