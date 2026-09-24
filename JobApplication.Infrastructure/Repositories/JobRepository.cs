using JobApplication.Domain.Entites;
using JobApplication.Infrastructure.Data;
using JobApplication.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace JobApplication.Infrastructure.Repositories
{
    public class JobRepository : IJobRepository
    {
        private readonly AppDbContext _context;
        public JobRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Job?> GetJobByIdAsync(int id)
        {
            return await _context.Jobs.AsNoTracking().FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<Job> CreateJobAsync(Job job)
        {
            await _context.Jobs.AddAsync(job);
            await _context.SaveChangesAsync();
            return job;
        }

        public async Task<Job> UpdateJobAsync(Job newJob)
        {
            _context.Jobs.Update(newJob);
            await _context.SaveChangesAsync();
            return newJob;
        }

        public async Task<bool> DeleteJobAsync(int id)
        {
            var entity = await _context.Jobs.FindAsync(id);
            if (entity == null) return false;
            _context.Jobs.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<Job>> GetActiveExpiredJobsAsync()
        {
            return await _context.Jobs
                .Where(j => j.IsActive
                         && j.ClosedAt.HasValue
                         && j.ClosedAt.Value <= DateTime.UtcNow)
                .ToListAsync();
        }

        /// <inheritdoc/>
        public async Task CloseJobsAsync(IEnumerable<Job> jobs, string closedBy)
        {
            var now = DateTime.UtcNow;
            foreach (var job in jobs)
            {
                job.IsActive = false;
                job.ClosedAt = now;
                job.ClosedBy = closedBy;
            }
            _context.Jobs.UpdateRange(jobs);
            await _context.SaveChangesAsync();
        }
    }
}
