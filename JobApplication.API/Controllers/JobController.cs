using JobApplication.Application.Features.Jobs.Commands.CloseJob;
using JobApplication.Application.Features.Jobs.Commands.CreateJob;
using JobApplication.Application.Features.Jobs.Commands.DeleteJob;
using JobApplication.Application.Features.Jobs.Commands.UpdateJob;
using JobApplication.Application.Features.Jobs.Queries.GetJobById;
using JobApplication.Domain.Entites;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JobApplication.API.Controllers
{
    /// <summary>
    /// Manages job postings — create, read, update, delete, and close operations.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class JobController : ControllerBase
    {
        private readonly IMediator _mediator;

        public JobController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>Gets a job posting by its ID.</summary>
        /// <param name="id">The job ID.</param>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Job), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetJobByIdAsync(int id)
        {
            var job = await _mediator.Send(new GetJobByIdQurey() { Id = id });
            if (job == null) return NotFound();
            return Ok(job);
        }

        /// <summary>Creates a new job posting.</summary>
        [HttpPost]
        [ProducesResponseType(typeof(Job), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateJobAsync(Job job)
        {
            var create = await _mediator.Send(new CreateJobCommand() { job = job });
            return Ok(create);
        }

        /// <summary>Updates an existing job posting.</summary>
        /// <param name="id">The job ID.</param>
        /// <param name="newJob">The updated job data.</param>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(Job), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateJobAsync(Job newJob, int id)
        {
            var updated = await _mediator.Send(new UpdateJobCommand() { id = id, newjob = newJob });
            if (updated == null) return NotFound();
            return Ok(updated);
        }

        /// <summary>Deletes a job posting.</summary>
        /// <param name="id">The job ID.</param>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteJobAsync(int id)
        {
            var delete = await _mediator.Send(new DeleteJobCommand() { id = id });
            if (delete == false) return NotFound();
            return Ok();
        }

        /// <summary>Closes an active job posting.</summary>
        /// <param name="id">The job ID.</param>
        [HttpPut("{id}/close")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> CloseJobAsync(int id)
        {
            var job = await _mediator.Send(new CloseJobCommand() { id = id });
            if (job == false) return NotFound();
            return Ok();
        }
    }
}