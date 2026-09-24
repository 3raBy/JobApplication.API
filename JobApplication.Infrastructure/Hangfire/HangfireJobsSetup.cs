using Hangfire;
using JobApplication.Application.Interfaces;

namespace JobApplication.Infrastructure.Hangfire;

/// <summary>
/// Registers all recurring Hangfire jobs for the Job Application system.
/// Call this once after <c>app.UseHangfireDashboard()</c> in Program.cs.
/// </summary>
public static class HangfireJobsSetup
{
    /// <summary>
    /// Registers all recurring jobs with their CRON schedules.
    /// </summary>
    /// <param name="recurringJobManager">Hangfire's recurring job manager.</param>
    public static void RegisterRecurringJobs(this IRecurringJobManager recurringJobManager)
    {
        // ── Job 1: Close expired job postings ──────────────────────────────────
        // Runs every day at 00:00 UTC
        recurringJobManager.AddOrUpdate<IRecurringJobService>(
            recurringJobId: "close-expired-jobs",
            methodCall: svc => svc.CloseExpiredJobsAsync(),
            cronExpression: Cron.Daily(),
            options: new RecurringJobOptions
            {
                TimeZone = TimeZoneInfo.Utc
            });

        // ── Job 2: Auto-reject stale pending applications ──────────────────────
        // Runs every 6 hours
        recurringJobManager.AddOrUpdate<IRecurringJobService>(
            recurringJobId: "auto-reject-stale-applications",
            methodCall: svc => svc.AutoRejectStaleApplicationsAsync(),
            cronExpression: "0 */6 * * *",
            options: new RecurringJobOptions
            {
                TimeZone = TimeZoneInfo.Utc
            });
    }
}
