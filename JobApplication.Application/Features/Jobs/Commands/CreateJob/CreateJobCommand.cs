using JobApplication.Domain.Entites;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace JobApplication.Application.Features.Jobs.Commands.CreateJob
{
    public class CreateJobCommand : IRequest<Job>
    {
        public Job job { get; set; }
    }
}
