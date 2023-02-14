using BLL.Managers;
using BLL.Models.Skill;
using Microsoft.AspNetCore.Mvc;

namespace CVGeneratorV1.Controllers
{
    [Produces("application/json")]
    [ApiController]
    [Route("api/[controller]")]
    public class SkillController : ControllerBase
    {
        private readonly SkillManager _skillManager;

        public SkillController(SkillManager skillManager)
        {
            _skillManager = skillManager;
        }

        [HttpGet("{id}", Name = "GetSkill")]
        public async Task<ActionResult<SkillDto>> GetSkillAsync(int id, CancellationToken cancellation)
        {
            return Ok(await _skillManager.GetSkillAsync(id, cancellation));
        }

        [HttpGet]
        public async Task<ActionResult<List<SkillDto>>> GetSkillsAsync(CancellationToken cancellation)
        {
            return Ok(await _skillManager.GetSkillsAsync(cancellation));
        }

        [HttpGet("deleted")]
        public async Task<ActionResult<List<SkillDto>>> GetDeletedSkillsForPersonAsync(int personId, CancellationToken cancellation)
        {
            return Ok(await _skillManager.GetDeletedSkillsForPersonAsync(personId, cancellation));
        }

        [HttpPost]
        public async Task<ActionResult<SkillDto>> CreateSkillAsync(SkillCreateAndUpdateDto skillDtoCreate, CancellationToken cancellation)
        {
            var createdSkillResult = await _skillManager.CreateSkillAsync(skillDtoCreate, cancellation);
            if (createdSkillResult.IsSuccesfull)
                return CreatedAtRoute("GetSkill", new { id = createdSkillResult.Data.Id }, createdSkillResult);
            return BadRequest(createdSkillResult);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<SkillDto>> UpdateSkillAsync(int id, SkillCreateAndUpdateDto skillDtoUpdate, CancellationToken cancellation)
        {
            var updatedSkillResult = await _skillManager.UpdateSkillAsync(id, skillDtoUpdate, cancellation);
            if (updatedSkillResult.IsSuccesfull)
                return CreatedAtRoute("GetSkill", new { id = updatedSkillResult.Data.Id }, updatedSkillResult);
            return BadRequest(updatedSkillResult);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteSkillAsync(int id, CancellationToken cancellation)
        {
            await _skillManager.DeleteSkillAsync(id, cancellation);
            return Ok();
        }

        [HttpDelete("{id}/restore")]
        public async Task<ActionResult> RetrieveSkillAsync(int id, CancellationToken cancellation)
        {
            await _skillManager.RetrieveSkillAsync(id, cancellation);
            return Ok();
        }
    }
}
