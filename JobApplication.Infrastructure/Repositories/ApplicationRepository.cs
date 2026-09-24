using JobApplication.Application.Interfaces;
using JobApplication.Domain.Enums;
using JobApplication.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace JobApplication.Infrastructure.Repositories
{
    public class ApplicationRepository : IApplicationRepository
    {
        private readonly AppDbContext _context;
        public ApplicationRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<JobApplication.Domain.Entites.Application?> GetApplicationByIdAsync(int id)
        {
            return await _context.Applications.AsNoTracking().FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<JobApplication.Domain.Entites.Application> UpdateApplicationAsync(JobApplication.Domain.Entites.Application newapplication)
        {
            _context.Applications.Update(newapplication);
            await _context.SaveChangesAsync();
            return newapplication;
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<JobApplication.Domain.Entites.Application>> GetStalePendingApplicationsAsync(int olderThanDays)
        {
            var cutoff = DateTime.UtcNow.AddDays(-olderThanDays);
            return await _context.Applications
                .Where(a => a.ApplicationStatus == ApplicationStatus.Applied
                         && a.StatusUpdatedAt < cutoff)
                .ToListAsync();
        }

        /// <inheritdoc/>
        public async Task BulkUpdateApplicationsAsync(IEnumerable<JobApplication.Domain.Entites.Application> applications)
        {
            _context.Applications.UpdateRange(applications);
            await _context.SaveChangesAsync();
        }
    }
}
