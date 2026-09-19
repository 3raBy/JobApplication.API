using JobApplication.Application.Services;
using JobApplication.Domain.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JobApplication.API.Controllers
{
    [Route("api/applications")]
    [ApiController]
    public class ApplicationController : ControllerBase
    {
        private readonly ApplicationService _service;
        public ApplicationController(ApplicationService service)
        {
            _service = service;
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> CancelApplicationAsync(int id)
        {
            var app = await _service.CancelApplicationAsync(id);
            if (app == false) return NotFound();
            return Ok(app);
        }
        [HttpPut("{id}/status")]
        public async Task<IActionResult> UpdateStatusAsync(int id, ApplicationStatus newStatus)
        {
            var result = await _service.UpdateStatusAsync(id, newStatus);

            if (!result)
                return BadRequest();

            return Ok();
        }
    }
}
