using JobApplication.Application.Interfaces;
using JobApplication.Domain.Entites;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace JobApplication.Application.Features.Jobs.Commands.CreateJob
{
    public class CreateJobHandler : IRequestHandler<CreateJobCommand, Job>
    {
        private readonly IJobRepository _jobRepository;
        public CreateJobHandler(IJobRepository jobRepository)
        {
            _jobRepository = jobRepository;
        }
        public async Task<Job> Handle(CreateJobCommand request, CancellationToken cancellationToken)
        {
            return await _jobRepository.CreateJobAsync(request.job);

        }
    }
}
