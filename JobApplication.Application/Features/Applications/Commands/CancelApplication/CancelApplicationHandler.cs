using JobApplication.Application.Interfaces;
using JobApplication.Domain.Enums;
using MediatR;

namespace JobApplication.Application.Features.Applications.Commands.CancelApplication
{
    public class CancelApplicationCommandHandler
        : IRequestHandler<CancelApplicationCommand, bool>
    {
        private readonly IApplicationRepository _repository;

        public CancelApplicationCommandHandler(
            IApplicationRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(
            CancelApplicationCommand request,
            CancellationToken cancellationToken)
        {
            var app = await _repository.GetApplicationByIdAsync(request.Id);

            if (app == null)
                return false;

            if (app.ApplicationStatus != ApplicationStatus.Applied &&
                app.ApplicationStatus != ApplicationStatus.UnderReview)
            {
                return false;
            }

            app.ApplicationStatus = ApplicationStatus.Cancelled;
            app.CancelledAt = DateTime.Now;

            await _repository.UpdateApplicationAsync(app);

            return true;
        }
    }
}
