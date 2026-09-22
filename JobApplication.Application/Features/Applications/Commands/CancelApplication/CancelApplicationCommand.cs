using MediatR;

namespace JobApplication.Application.Features.Applications.Commands.CancelApplication
{
    public class CancelApplicationCommand : IRequest<bool>
    {
        public int Id { get; set; }
    }
}
