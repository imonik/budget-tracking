using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using FinanceAndBudgetTracking.Data;
using FinanceAndBudgetTracking.DTO;
using FinanceAndBudgetTracking.Models;
using FinanceAndBudgetTracking.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinanceAndBudgetTracking.Controllers
{
    [Authorize]
    [Route("api/budgets")]
    [ApiController]
    public class BudgetController : ControllerBase
    {
        private readonly IBudgetRepository _budgetRepository;
        private readonly AuthService _auth;
        private readonly JwtService _jwtService;
        public BudgetController(IBudgetRepository budgetRepository, AuthService auth, JwtService jwtService)
        {
            _auth = auth;
            _budgetRepository = budgetRepository;
            _jwtService = jwtService;
        }

        [HttpGet("getall")]   
        public async Task<IActionResult> GetAllBudgets()
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
            var budgets = await _budgetRepository.GetAllBudgetsByUser(int.Parse(userId));
            return Ok(budgets);
        }

        [HttpGet("getById/{id}")]
        public async Task<IActionResult> GetBudgetById(int id)
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
            var budget = await _budgetRepository.GetBudgetById(id);
            return Ok(budget);
        }

        [HttpPost]
        public async Task<IActionResult> AddBudget([FromBody] BudgetDTO budget)
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
            var AppUserId = int.Parse(userId);
            var newBudget = new Budget
            {
                UserId = AppUserId,
                CategoryId = budget.CategoryId,
                Amount = budget.Amount,
                CreatedOn = DateTime.Now,
                ModifiedOn = budget.ModifiedOn
            };
            newBudget = await _budgetRepository.AddBudget(newBudget);
            return Ok(newBudget);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateBudget([FromBody] Budget budget)
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
            budget.UserId = int.Parse(userId);
            var updatedBudget = await _budgetRepository.UpdateBudget(budget);
            return Ok(updatedBudget);
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteBudget(int id)
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
            var deletedBudget = await _budgetRepository.DeleteBudget(id);
            return Ok(deletedBudget);
        }
        //[HttpPost]
        //public async Task<IActionResult> GetBudgetsByUser(int userId)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }
        //    var budgets = await _budgetRepository.GetAllBudgetsByUser(userId);
        //    return Ok(budgets);
        //}
        [HttpGet("getbudgetbycategory/{categoryId}")]
        public async Task<IActionResult> GetBudgetsByCategory(int categoryId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var budgets = await _budgetRepository.GetAllBudgetsByCategory(categoryId);
            return Ok(budgets);
        }
    }
}
