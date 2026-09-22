using JobApplication.Application.Services;
using JobApplication.Domain.Enums;
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
        private readonly ApplicationService _service;

        public ApplicationController(ApplicationService service)
        {
            _service = service;
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
            var app = await _service.CancelApplicationAsync(id);
            if (app == false) return NotFound();
            return Ok(app);
        }

        /// <summary>
        /// Updates the status of an existing job application (e.g. Pending → Approved).
        /// </summary>
        /// <param name="id">The unique identifier of the application to update.</param>
        /// <param name="newStatus">The new status to assign to the application.</param>
        /// <returns>No content on success.</returns>
        /// <response code="200">The application status was updated successfully.</response>
        /// <response code="400">The status transition is invalid or the application was not found.</response>
        [HttpPut("{id}/status")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateStatusAsync(int id, ApplicationStatus newStatus)
        {
            var result = await _service.UpdateStatusAsync(id, newStatus);

            if (!result)
                return BadRequest();

            return Ok();
        }
    }
}
