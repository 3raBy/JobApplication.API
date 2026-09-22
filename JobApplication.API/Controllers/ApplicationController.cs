using JobApplication.Application.Features.Applications.Commands.CancelApplication;
using JobApplication.Application.Features.Applications.Commands.UpdateApplicationStatus;
using JobApplication.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JobApplication.API.Controllers
{
    /// <summary>
    /// Manages job applications — cancellation and status update operations.
    /// </summary>
    [Route("api/applications")]
    [ApiController]
    [Produces("application/json")]
    public class ApplicationController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ApplicationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Cancels an existing job application.
        /// </summary>
        /// <param name="id">The unique identifier of the application to cancel.</param>
        /// <returns>True if the application was cancelled successfully.</returns>
        /// <response code="200">The application was cancelled successfully.</response>
        /// <response code="404">No application found with the specified ID.</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> CancelApplicationAsync(int id)
        {
            var result = await _mediator.Send(
                new CancelApplicationCommand
                {
                    Id = id
                });

            if (!result)
                return NotFound();

            return Ok(result);
        }

        /// <summary>
        /// Updates the status of an existing job application.
        /// </summary>
        /// <param name="id">The unique identifier of the application to update.</param>
        /// <param name="newStatus">The new status to assign to the application.
        /// </param>
        /// <response code="200">The application status was updated successfully.</response>
        /// <response code="400">The status transition is invalid.</response>
        [HttpPut("{id}/status")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateStatusAsync(
            int id,
            ApplicationStatus newStatus)
        {
            var result = await _mediator.Send(
                new UpdateApplicationStatusCommand
                {
                    Id = id,
                    NewStatus = newStatus
                });

            if (!result)
                return BadRequest();

            return Ok();
        }
    }
}
