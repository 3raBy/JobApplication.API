using MediatR;
using Microsoft.AspNetCore.Http.Features;
using System;
using System.Collections.Generic;
using System.Text;

namespace JobApplication.Application.Features.Jobs.Commands.CloseJob
{
    public class CloseJobCommand : IRequest<bool>
    {
        public int id { get; set; }
    }
}
