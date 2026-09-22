using JobApplication.Application.Interfaces;
using JobApplication.Domain.Enums;
using MediatR;

namespace JobApplication.Application.Features.Applications.Commands.UpdateApplicationStatus
{
    public class UpdateApplicationStatusCommandHandler
        : IRequestHandler<UpdateApplicationStatusCommand, bool>
    {
        private readonly IApplicationRepository _repository;

        public UpdateApplicationStatusCommandHandler(
            IApplicationRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(
            UpdateApplicationStatusCommand request,
            CancellationToken cancellationToken)
        {
            var app = await _repository.GetApplicationByIdAsync(request.Id);

            if (app == null)
                return false;

            if (request.NewStatus <= app.ApplicationStatus)
                return false;

            if (request.NewStatus == ApplicationStatus.Cancelled)
                return false;

            app.ApplicationStatus = request.NewStatus;
            app.StatusUpdatedAt = DateTime.Now;

            await _repository.UpdateApplicationAsync(app);

            return true;
        }
    }
}
