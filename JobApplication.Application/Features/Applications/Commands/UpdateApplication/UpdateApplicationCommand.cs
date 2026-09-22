using JobApplication.Domain.Enums;
using MediatR;

namespace JobApplication.Application.Features.Applications.Commands.UpdateApplicationStatus
{
    public class UpdateApplicationStatusCommand : IRequest<bool>
    {
        public int Id { get; set; }
        public ApplicationStatus NewStatus { get; set; }
    }
}
