using BLL.Managers;
using BLL.Models.Person;
using Microsoft.AspNetCore.Mvc;

namespace CVGeneratorV1.Controllers
{
    [Produces("application/json")]
    [ApiController]
    [Route("api/[controller]")]
    public class PersonController : ControllerBase
    {
        private readonly PersonManager _personManager;

        public PersonController(PersonManager personManager)
        {
            _personManager = personManager;
        }

        [HttpGet("{id}", Name = "GetPerson")]
        public async Task<ActionResult<PersonDto>> GetPersonAsync(int id, CancellationToken cancellation)
        {
            return Ok(await _personManager.GetPersonAsync(id, cancellation));
            //var gottenPersonResult = await _personManager.GetPersonAsync(id, cancellation);
            //if (gottenPersonResult.IsSuccesfull)
            //    return Ok();
            //return BadRequest(gottenPersonResult);
        }

        [HttpGet]
        public async Task<ActionResult<List<PersonSearchDto>>> GetPersonsNamesAndIdsAsync(CancellationToken cancellation)
        {
            return Ok(await _personManager.GetPersonNamesAndIdsAsync(cancellation));
        }

        [HttpGet("deleted")]
        public async Task<ActionResult<List<PersonSearchDto>>> GetPersonsNamesAndIdsIncluDeletedAsync(CancellationToken cancellation)
        {
            return Ok(await _personManager.GetPersonNamesAndIdsIncludeDeletedAsync(cancellation));
        }

        [HttpPost]
        public async Task<ActionResult<PersonDto>> CreatePersonAsync(PersonCreateAndUpdateDto personForCreationDto, CancellationToken cancellation)
        {
            var createdPersonResult = await _personManager.CreatePersonAsync(personForCreationDto, cancellation);
            if (createdPersonResult.IsSuccesfull)
                return CreatedAtRoute("GetPerson", new { id = createdPersonResult.Data.Id }, createdPersonResult);
            return BadRequest(createdPersonResult);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<PersonDto>> UpdatePersonAsync(int id, PersonCreateAndUpdateDto personDtoForUpdate, CancellationToken cancellation)
        {
            var updatedPersonResult = await _personManager.UpdatePersonAsync(id, personDtoForUpdate, cancellation);
            if (updatedPersonResult.IsSuccesfull)
                return CreatedAtRoute("GetPerson", new { id = updatedPersonResult.Data.Id }, updatedPersonResult);
            return BadRequest(updatedPersonResult);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletePersonAsync(int id, CancellationToken cancellation)
        {
            await _personManager.DeletePersonAsync(id, cancellation);
            return Ok();
        }

        [HttpDelete("{id}/restore")]
        public async Task<ActionResult> RetrievePersonAsync(int id, CancellationToken cancellation)
        {
            await _personManager.RetrievePersonAsync(id, cancellation);
            return Ok();
        }
    }
}
