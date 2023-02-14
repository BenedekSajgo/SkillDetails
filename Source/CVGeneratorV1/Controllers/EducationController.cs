using BLL.Managers;
using BLL.Models.Education;
using Microsoft.AspNetCore.Mvc;

namespace CVGeneratorV1.Controllers
{
    [Produces("application/json")]
    [ApiController]
    [Route("api/person/{personId}/[controller]")]
    public class EducationController : ControllerBase
    {
        private readonly EducationManager _educationManager;

        public EducationController(EducationManager educationManager)
        {
            _educationManager = educationManager;
        }

        [HttpGet("{id}", Name = "GetEducation")]
        public async Task<ActionResult<EducationDto>> GetEducationAsync(int id, CancellationToken cancellation)
        {
            return Ok(await _educationManager.GetEducationAsync(id, cancellation));
        }

        [HttpGet("deleted")]
        public async Task<ActionResult<List<EducationDto>>> GetDeletedEducationsForPersonAsync(int personId, CancellationToken cancellation)
        {
            return Ok(await _educationManager.GetDeletedEducationsForPersonAsync(personId, cancellation));
        }

        [HttpPost]
        public async Task<ActionResult<EducationDto>> CreateEducationAsync(int personId, EducationCreateAndUpdateDto educationUpdateDto, CancellationToken cancellation)
        {
            var createdEducationResult = await _educationManager.CreateEducationAsync(personId, educationUpdateDto, cancellation);
            if (createdEducationResult.IsSuccesfull)
                return CreatedAtRoute("GetEducation", new { personId, id = createdEducationResult.Data.Id }, createdEducationResult);
            return BadRequest(createdEducationResult);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<EducationDto>> UpdateEducatonAsync(int id, EducationCreateAndUpdateDto educationUpdateDto, CancellationToken cancellation)
        {
            var updatedEducatioResult = await _educationManager.UpdateEducationAsync(id, educationUpdateDto, cancellation);
            if (updatedEducatioResult.IsSuccesfull)
                return CreatedAtRoute("GetEducation", new { id = updatedEducatioResult.Data.Id, updatedEducatioResult});
            return BadRequest(updatedEducatioResult);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteEducationAsync(int id, CancellationToken cancellation)
        {
            await _educationManager.DeleteEducationAsync(id, cancellation);
            return Ok();
        }

        [HttpDelete("{id}/restore")]
        public async Task<ActionResult> RetrieveEducationAsync(int id, CancellationToken cancellation)
        {
            await _educationManager.RetrieveEducationAsync(id, cancellation);
            return Ok();
        }
    }
}
