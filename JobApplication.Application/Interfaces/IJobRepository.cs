using JobApplication.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Text;

namespace JobApplication.Application.Interfaces
{
    public interface IJobRepository
    {
        Task<Job?> GetJobByIdAsync(int id);
        Task<Job> CreateJobAsync(Job job);
        Task<Job> UpdateJobAsync(Job newJob);
        Task<bool> DeleteJobAsync(int id);

        /// <summary>Returns all active jobs that have a ClosedAt date in the past.</summary>
        Task<IEnumerable<Job>> GetActiveExpiredJobsAsync();

        /// <summary>Bulk-close a list of jobs atomically.</summary>
        Task CloseJobsAsync(IEnumerable<Job> jobs, string closedBy);
    }
}
