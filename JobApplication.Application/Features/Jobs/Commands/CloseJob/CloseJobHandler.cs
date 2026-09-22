using JobApplication.Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace JobApplication.Application.Features.Jobs.Commands.CloseJob
{
    public class CloseJobHandler : IRequestHandler<CloseJobCommand , bool>
    {
        private readonly IJobRepository _repository;
        public CloseJobHandler(IJobRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(CloseJobCommand request, CancellationToken cancellationToken)
        {
            var job = await _repository.GetJobByIdAsync(request.id);
            if (job == null) { return false; }
            else
            {
                job.IsActive = false;
                job.ClosedAt = DateTime.Now;
                job.ClosedBy = "Recriuter";
            }
            await _repository.UpdateJobAsync(job);
            return true;
        }
    }
}
