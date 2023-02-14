using BLL.Managers;
using BLL.Models.Project;
using DAL.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CVGeneratorV1.Controllers
{
    [Produces("application/json")]
    [ApiController]
    [Route("api/[controller]")]
    public class ProjectController : ControllerBase
    {
        private readonly ProjectManager _projectManager;

        public ProjectController(ProjectManager projectManager)
        {
            _projectManager = projectManager;
        }

        [HttpGet("{id}", Name = "GetProject")]
        public async Task<ActionResult<ProjectDto>> GetProjectAsync(int id, CancellationToken cancellation)
        {
            return Ok(await _projectManager.GetProjectAsync(id, cancellation));
        }

        [HttpGet]
        public async Task<ActionResult<List<ProjectDto>>> GetProjectsAsync(CancellationToken cancellation)
        {
            return Ok(await _projectManager.GetProjectsAsync(cancellation));
        }

        [HttpGet("{personId}/deleted")]
        public async Task<ActionResult<List<ProjectDto>>> GetDeletedProjectsForPersonAsync(int personId, CancellationToken cancellation)
        {
            return Ok(await _projectManager.GetDeletedProjectsForPersonAsync(personId, cancellation));
        }

        [HttpPost("{personId}")]
        public async Task<ActionResult<ProjectDto>> CreateProjectAsync(int personId, ProjectCreateAndUpdateDto projectUpdateDto, CancellationToken cancellation)
        {
            var createdProjectResult = await _projectManager.CreateProjectAsync(personId, projectUpdateDto, cancellation);
            if (createdProjectResult.IsSuccesfull)
                return CreatedAtRoute("GetProject", new {personId, id = createdProjectResult.Data.Id}, createdProjectResult);
            return BadRequest(createdProjectResult);
        }

        [HttpPut]
        public async Task<ActionResult<ProjectDto>> UpdateProjectAsync(int id, ProjectCreateAndUpdateDto projectUpdateDto, CancellationToken cancellation)
        {
            var updatedProjectResult = await _projectManager.UpdateProjectAsync(id, projectUpdateDto, cancellation);
            if (updatedProjectResult.IsSuccesfull)
                return CreatedAtRoute("GetProject", new { id = updatedProjectResult.Data.Id}, updatedProjectResult);
            return BadRequest(updatedProjectResult);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProjectAsync(int id, CancellationToken cancellation)
        {
            await _projectManager.DeleteProjectAsync(id, cancellation);
            return Ok();
        }

        [HttpDelete("{id}/restore")]
        public async Task<ActionResult> RetrieveProjectAsync(int id, CancellationToken cancellation)
        {
            await _projectManager.RetrieveProjectAsync(id, cancellation);
            return Ok();
        }
    }
}
