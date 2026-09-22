using JobApplication.Application.Interfaces;
using JobApplication.Domain.Entites;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace JobApplication.Application.Features.Jobs.Queries.GetJobById
{
    public class GetJobByIdHandler : IRequestHandler<GetJobByIdQurey, Job?>
    {
        private readonly IJobRepository _jobRepository;
        public GetJobByIdHandler(IJobRepository jobRepository)
        {
            _jobRepository = jobRepository;
        }
        public async Task<Job?> Handle(GetJobByIdQurey request, CancellationToken cancellationToken)
        {
            return await _jobRepository.GetJobByIdAsync(request.Id);
        }
    }
}
