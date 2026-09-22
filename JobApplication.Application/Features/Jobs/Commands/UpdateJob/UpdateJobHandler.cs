using JobApplication.Application.Interfaces;
using JobApplication.Domain.Entites;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace JobApplication.Application.Features.Jobs.Commands.UpdateJob
{
    public class UpdateJobHandler : IRequestHandler<UpdateJobCommand, Job?>
    {
        private readonly IJobRepository _jobRepository;
        public UpdateJobHandler(IJobRepository jobRepository)
        {
            _jobRepository = jobRepository;
        }
        public async Task<Job?> Handle(UpdateJobCommand request, CancellationToken cancellationToken)
        {
            var job = await _jobRepository.GetJobByIdAsync(request.id);
            if(job == null) { return null; }
            request.newjob.Id = request.id;
            return await _jobRepository.UpdateJobAsync(request.newjob);
        }
    }
}
