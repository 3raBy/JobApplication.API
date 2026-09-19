using JobApplication.Application.Services;
using JobApplication.Domain.Entites;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JobApplication.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobController : ControllerBase
    {
        private readonly JobService _jobService;
        public JobController(JobService jobService)
        {
            _jobService = jobService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetJobByIdAsync(int id)
        {
            var job = await _jobService.GetJobByIdAsync(id);
            if (job == null) return NotFound();
            return Ok(job);

        }
        [HttpPost]
        public async Task<IActionResult> CreateJobAsync(Job job)
        {
            var create = await _jobService.CreateJobAsync(job);
            return Ok(create);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateJobAsync(Job newJob , int id)
        {
            var job = await _jobService.GetJobByIdAsync(id);
            if (job == null)
            {
                return NotFound();

            }
            newJob.Id = id;
            await _jobService.UpdateJobAsync(newJob);
            return Ok();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteJobAsync(int id)
        {
            var delete = await _jobService.DeleteJobAsync(id);
            if (delete == false) return NotFound();
            return Ok();
        }
        [HttpPut("{id}/close")]
        public async Task<IActionResult> CloseJobAsync(int id)
        {
            var job = await _jobService.CloseJobAsync(id);
            if (job == false) return NotFound();
            return Ok();
        }

    }
}
