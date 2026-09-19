using JobApplication.Application.Interfaces;
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
    }
}
