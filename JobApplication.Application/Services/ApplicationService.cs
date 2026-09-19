using JobApplication.Application.Interfaces;
using JobApplication.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace JobApplication.Application.Services
{
    public class ApplicationService
    {
        private readonly IApplicationRepository _repository;
        public ApplicationService(IApplicationRepository repository)
        {
            _repository = repository;
        }
        public async Task<bool> CancelApplicationAsync(int id)
        {
            var app = await _repository.GetApplicationByIdAsync(id);
            if (app == null) return false;
            else
            {
                if(app.ApplicationStatus != Domain.Enums.ApplicationStatus.Applied && app.ApplicationStatus != Domain.Enums.ApplicationStatus.UnderReview)
                {
                    return false;
                }
                app.ApplicationStatus = Domain.Enums.ApplicationStatus.Cancelled;
                app.CancelledAt = DateTime.Now;
            }
            await _repository.UpdateApplicationAsync(app);
            return true;
        }
        public async Task<bool> UpdateStatusAsync(int id, ApplicationStatus newStatus)
        {
            var app = await _repository.GetApplicationByIdAsync(id);

            if (app == null)
                return false;

            if (newStatus <= app.ApplicationStatus)
                return false;

            if (newStatus == ApplicationStatus.Cancelled)
                return false;

            app.ApplicationStatus = newStatus;
            app.StatusUpdatedAt = DateTime.Now;

            await _repository.UpdateApplicationAsync(app);

            return true;
        }
    }
}
