using System;
using System.Collections.Generic;
using System.Text;

namespace JobApplication.Application.Interfaces
{
    public interface IApplicationRepository
    {
        Task<JobApplication.Domain.Entites.Application?> GetApplicationByIdAsync(int id);
        Task<JobApplication.Domain.Entites.Application> UpdateApplicationAsync(JobApplication.Domain.Entites.Application newapplication);

        /// <summary>
        /// Returns all applications that are still in <c>Applied</c> (initial) status and have not been
        /// updated for more than <paramref name="olderThanDays"/> days.
        /// </summary>
        Task<IEnumerable<JobApplication.Domain.Entites.Application>> GetStalePendingApplicationsAsync(int olderThanDays);

        /// <summary>Bulk-update a list of applications in a single SaveChanges call.</summary>
        Task BulkUpdateApplicationsAsync(IEnumerable<JobApplication.Domain.Entites.Application> applications);
    }
}
