using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using FinanceAndBudgetTracking.DataLayer.Interfaces;
using FinanceAndBudgetTracking.DataLayer.Entities;
using FinanceAndBudgetTracking.Models.DTO;
using FinanceAndBudgetTracking.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using FinanceAndBudgetTracking.API.Services;

namespace FinanceAndBudgetTracking.Controllers
{
    [Authorize(Policy ="UserMustHaveId")]
    [Route("api/categories")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly ICurrentUserService _currentUser;

        public CategoryController(ICategoryRepository categoryRepository, ICurrentUserService currentUser)
        {
            _categoryRepository = categoryRepository;
            _currentUser = currentUser;
        }

        [HttpGet("getallbyuser")]
        public async Task<IActionResult> GetAllCategories()
        {
            var userId = _currentUser.GetUserId();

            var categories = await _categoryRepository.GetAllCategoriesByUser((userId.Value));

            return Ok(categories);
        }

        [HttpGet("getall")]
        public async Task<IActionResult> GetGeneralCategories()
        {
            var userId = _currentUser.GetUserId();
            if (userId == null) return Unauthorized();

            var category = await _categoryRepository.GetAllGeneralCategories();
            return Ok(category);
        }

        [HttpGet("category/{id}")]
        public async Task<IActionResult> GetCategoryById(int id)
        {
            var userId = _currentUser.GetUserId();
            if (userId == null) return Unauthorized();

            var category = await _categoryRepository.GetCategoryById(id);
            return Ok(category);
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddCategory([FromBody] UserCategoryDTO category)
        {
            var userId = _currentUser.GetUserId();
            if (userId == null) return Unauthorized();

            var newcategory = new UserCategory { Name = category.Name, UserId = userId.Value, CreatedOn = DateTime.Now, ModifiedOn = DateTime.Now };
            var savedCategory = await _categoryRepository.AddCategory(newcategory);
            return Ok(savedCategory);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCategory([FromBody] UserCategoryDTO category)
        {
            var userId = _currentUser.GetUserId();
            if (userId == null) return Unauthorized();

            var updatedCategory = new UserCategory { CategoryId = category.CategoryId, Name = category.Name, UserId = userId.Value, ModifiedOn = DateTime.Now };
            var savedCategory = await _categoryRepository.UpdateCategory(updatedCategory);
            return Ok(savedCategory);
        }

        [HttpDelete("detele/{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {

            var userId = _currentUser.GetUserId();
            if (userId == null) return Unauthorized();

            var deletedCategory = await _categoryRepository.DeleteCategory(id);
            return Ok(deletedCategory);
        }
    }
}
