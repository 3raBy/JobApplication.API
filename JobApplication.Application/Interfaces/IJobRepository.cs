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
    }
}
