using System;
using System.Collections.Generic;
using System.Text;

namespace JobApplication.Application.Interfaces
{
    public interface IApplicationRepository
    {
        Task<JobApplication.Domain.Entites.Application?> GetApplicationByIdAsync(int id);
        Task<JobApplication.Domain.Entites.Application> UpdateApplicationAsync(JobApplication.Domain.Entites.Application newapplication);
    }
}
