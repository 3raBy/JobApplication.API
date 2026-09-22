using JobApplication.Domain.Entites;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using JobApplication.Application.Features.Jobs.Queries.GetJobById;
using JobApplication.Application.Features.Jobs.Commands.CreateJob;
using JobApplication.Application.Features.Jobs.Commands.UpdateJob;
using JobApplication.Application.Features.Jobs.Commands.DeleteJob;
using JobApplication.Application.Features.Jobs.Commands.CloseJob;

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

        /// <summary>
        /// Retrieves a single job posting by its unique identifier.
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Job), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetJobByIdAsync(int id)
        {
            var job = await _mediator.Send(new GetJobByIdQurey() { Id = id });

            if (job == null)
                return NotFound();

            return Ok(job);
        }

        /// <summary>
        /// Creates a new job posting.
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(Job), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateJobAsync(Job job)
        {
            var create = await _mediator.Send(new CreateJobCommand() { job = job });

            return Ok(create);
        }

        /// <summary>
        /// Updates an existing job posting.
        /// </summary>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateJobAsync(Job newJob, int id)
        {
            var job = await _mediator.Send(
                new UpdateJobCommand() { id = id, newjob = newJob });

            if (job == null)
                return NotFound();

            return Ok();
        }

        /// <summary>
        /// Permanently deletes a job posting by its unique identifier.
        /// </summary>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteJobAsync(int id)
        {
            var delete = await _mediator.Send(
                new DeleteJobCommand() { id = id });

            if (delete == false)
                return NotFound();

            return Ok();
        }

        /// <summary>
        /// Closes a job posting.
        /// </summary>
        [HttpPut("{id}/close")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> CloseJobAsync(int id)
        {
            var job = await _mediator.Send(
                new CloseJobCommand() { id = id });

            if (job == false)
                return NotFound();

            return Ok();
        }
    }
}

