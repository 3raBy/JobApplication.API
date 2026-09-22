using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace JobApplication.Application.Features.Jobs.Commands.DeleteJob
{
    public class DeleteJobCommand : IRequest<bool>
    {
        public int id { get; set; }
    }
}
