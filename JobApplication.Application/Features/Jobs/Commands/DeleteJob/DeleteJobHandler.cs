using System;
using System.Collections.Generic;
using System.Text;
using JobApplication.Application.Interfaces;
using MediatR;

namespace JobApplication.Application.Features.Jobs.Commands.DeleteJob
{
    public class DeleteJobHandler : IRequestHandler<DeleteJobCommand, bool>
    {
        private readonly IJobRepository _jobRepository;
        public DeleteJobHandler(IJobRepository jobRepository)
        {
            _jobRepository = jobRepository;
        }
        public async Task<bool> Handle(DeleteJobCommand request, CancellationToken cancellationToken)
        {
            return await _jobRepository.DeleteJobAsync(request.id);
        }
    }
}
