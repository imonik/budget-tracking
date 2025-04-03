using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using FinanceAndBudgetTracking.DataLayer.Interfaces;
using FinanceAndBudgetTracking.DataLayer.Entities;
using FinanceAndBudgetTracking.Models.DTO;
using FinanceAndBudgetTracking.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinanceAndBudgetTracking.Controllers
{
    [Authorize]
    [Route("api/categories")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly AuthService _auth;
        private readonly JwtService _jwtService;

        public CategoryController(ICategoryRepository categoryRepository, AuthService auth, JwtService jwtService)
        {
            _auth = auth;
            _categoryRepository = categoryRepository;
            _jwtService = jwtService;
        }
        [HttpGet("getallbyuser")]
        public async Task<IActionResult> GetAllCategories()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? User.FindFirstValue(JwtRegisteredClaimNames.Sub);

            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized("User ID not found in JWT token.");
            }

            var categories = await _categoryRepository.GetAllCategoriesByUser(int.Parse(userId));

            return Ok(categories);
        }

        [HttpGet("getall")]
        public async Task<IActionResult> GetGeneralCategories()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? User.FindFirstValue(JwtRegisteredClaimNames.Sub);
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized("User ID not found in JWT token.");
            }
            var category = await _categoryRepository.GetAllGeneralCategories();
            return Ok(category);
        }

        [HttpGet("category/{id}")]
        public async Task<IActionResult> GetCategoryById(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? User.FindFirstValue(JwtRegisteredClaimNames.Sub);
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized("User ID not found in JWT token.");
            }
            var category = await _categoryRepository.GetCategoryById(id);
            return Ok(category);
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddCategory([FromBody] UserCategoryDTO category)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? User.FindFirstValue(JwtRegisteredClaimNames.Sub);
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized("User ID not found in JWT token.");
            }
            category.UserId = int.Parse(userId);
            var newcategory = new UserCategory { Name = category.Name, UserId = category.UserId, CreatedOn = DateTime.Now, ModifiedOn = DateTime.Now };
            var savedCategory = await _categoryRepository.AddCategory(newcategory);
            return Ok(savedCategory);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCategory([FromBody] UserCategoryDTO category)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? User.FindFirstValue(JwtRegisteredClaimNames.Sub);
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized("User ID not found in JWT token.");
            }
            category.UserId = int.Parse(userId);
            var updatedCategory = new UserCategory { CategoryId = category.CategoryId, Name = category.Name, UserId = category.UserId, ModifiedOn = DateTime.Now };
            var savedCategory = await _categoryRepository.UpdateCategory(updatedCategory);
            return Ok(savedCategory);
        }

        [HttpDelete("detele/{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? User.FindFirstValue(JwtRegisteredClaimNames.Sub);
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized("User ID not found in JWT token.");
            }
            var deletedCategory = await _categoryRepository.DeleteCategory(id);
            return Ok(deletedCategory);
        }
    }
}
