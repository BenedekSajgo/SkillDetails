using BLL.Managers;
using BLL.Models.SkillLevel;
using DAL.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CVGeneratorV1.Controllers
{
    [Produces("application/json")]
    [ApiController]
    [Route("api/skill/{skillId}/[controller]")]
    public class SkillLevelController : ControllerBase
    {
        private readonly SkillLevelManager _skillLevelManager;

        public SkillLevelController(SkillLevelManager skillLevelManager)
        {
            _skillLevelManager = skillLevelManager;
        }

        [HttpGet("{id}", Name = "GetSkillLevel")]
        public async Task<ActionResult<SkillLevelDto>> GetSkillLevelAsync(int id, CancellationToken cancellation)
        {
            return Ok(await _skillLevelManager.GetSkillLevelAsync(id, cancellation));
        }

        [HttpGet("deleted")]
        public async Task<ActionResult<List<SkillLevelDto>>> GetDeletedSkillLevelsForSkillAsync(int skillId, CancellationToken cancellation)
        {
            return Ok(await _skillLevelManager.GetDeletedSkillLevelsForSkillAsync(skillId, cancellation));
        }

        [HttpPost]
        public async Task<ActionResult<SkillLevelDto>> CreateSkillLevelAsync(int skillId, SkillLevelCreateAndUpdateDto skillLevelCreateDto, CancellationToken cancellation)
        {
            var createdSkillLeveResult = await _skillLevelManager.CreateSkillLevelAsync(skillId, skillLevelCreateDto, cancellation);
            if (createdSkillLeveResult.IsSuccesfull)
                return CreatedAtRoute("GetSkillLevel", new { skillId, id = createdSkillLeveResult.Data.Id }, createdSkillLeveResult);
            return BadRequest(createdSkillLeveResult);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<SkillLevelDto>> UpdateSkillLevelAsync(int id, int skillId, SkillLevelCreateAndUpdateDto skillLevelUpdateDto, CancellationToken cancellation)
        {
            var updatedSkillLevelResult = await _skillLevelManager.UpdateSkillLevelAsync(id, skillId, skillLevelUpdateDto, cancellation);
            if (updatedSkillLevelResult.IsSuccesfull)
                return CreatedAtRoute("GetSkillLevel", new { skillId, id = updatedSkillLevelResult.Data.Id }, updatedSkillLevelResult);
            return BadRequest(updatedSkillLevelResult);
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteSkillLevelAsync(int id, CancellationToken cancellation)
        {
            await _skillLevelManager.DeleteSkillLevelAsync(id, cancellation);
            return Ok();
        }

        [HttpDelete("{id}/restore")]
        public async Task<ActionResult> RetrieveSkillLevelAsync(int id, CancellationToken cancellation)
        {
            await _skillLevelManager.RetrieveSkillLevelAsync(id, cancellation);
            return Ok();
        }

    }
}
