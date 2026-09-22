using JobApplication.Domain.Entites;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace JobApplication.Application.Features.Jobs.Commands.UpdateJob
{
    public class UpdateJobCommand : IRequest<Job?>
    {
        public int id { get; set; }
        public Job newjob { get; set; }
    }
}
