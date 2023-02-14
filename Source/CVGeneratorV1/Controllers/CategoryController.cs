using BLL.Managers;
using BLL.Models.Category;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CVGeneratorV1.Controllers
{
    [Produces("application/json")]
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly CategoryManager _categoryManager;

        public CategoryController(CategoryManager categoryManager)
        {
            _categoryManager = categoryManager;
        }

        [HttpGet("{id}", Name = "GetCategory")]
        public async Task<ActionResult<CategoryDto>> GetCategoryAsync(int id, CancellationToken cancellation)
        {
            return Ok(await _categoryManager.GetSkillCategoryAsync(id, cancellation));
        }

        [HttpGet("{id}/parent")]
        public async Task<ActionResult<CategoryDto>> GetParentCatogeryAsync(int id, CancellationToken cancellation)
        {
            return Ok(await _categoryManager.GetParentCategoryAsync(id, cancellation));
        }

        [HttpGet("{id}/children")]
        public async Task<ActionResult<List<CategoryDto>>> GetChildCategories(int id, CancellationToken cancellation)
        {
            return Ok(await _categoryManager.GetChildCategoriesAsync(id, cancellation));
        }

        [HttpGet("parents")]
        public async Task<ActionResult<List<CategoryDto>>> GetParentCategoriesAsync(CancellationToken cancellation)
        {
            return Ok(await _categoryManager.GetParentCategoriesAsync(cancellation));
        }

        [HttpPost]
        public async Task<ActionResult<CategoryDto>> CreateSkillCategoryAsync(CategoryCreateAndUpdateDto skillCategoryUpdateDto, CancellationToken cancellation)
        {
            var  createdCategoryResult = await _categoryManager.CreateSkillCategoryAsync(skillCategoryUpdateDto, cancellation);
            if (createdCategoryResult.IsSuccesfull)
                return CreatedAtRoute("GetCategory", new { id = createdCategoryResult.Data.Id }, createdCategoryResult);
            return BadRequest(createdCategoryResult);
        }

        [HttpPut]
        public async Task<ActionResult<CategoryDto>> UpdateSkillCategoryAsync(int id, CategoryCreateAndUpdateDto skillCategoryUpdateDto, CancellationToken cancellation)
        {
            var updatedCategoryResult = await _categoryManager.UpdateSkillCategoryAsync(id, skillCategoryUpdateDto, cancellation);
            if (updatedCategoryResult.IsSuccesfull)
                return CreatedAtRoute("GetCategory", new { id = updatedCategoryResult.Data.Id }, updatedCategoryResult);
            return BadRequest(updatedCategoryResult);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCategoryAsync(int id, CancellationToken cancellation)
        {
            await _categoryManager.DeleteCategoryAsync(id, cancellation);    
            return Ok();
        }

        [HttpDelete("{id}/restore")]
        public async Task<ActionResult> RetrieveCategoryAsync(int id, CancellationToken cancellation)
        {
            await _categoryManager.RetrieveCategoryAsync(id, cancellation);
            return Ok();
        }
    }
}
