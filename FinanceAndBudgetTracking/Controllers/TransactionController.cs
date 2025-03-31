using FinanceAndBudgetTracking.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using FinanceAndBudgetTracking.Services;
using FinanceAndBudgetTracking.Data;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using FinanceAndBudgetTracking.Models;


namespace FinanceAndBudgetTracking.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/tran")]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionRepository _transactionRepository;
        public TransactionController(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }

        [HttpGet("getall")]
        public async Task<IActionResult> GetTransactions()
        {
            //Get User Id from JWT tokens claims
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? User.FindFirstValue(JwtRegisteredClaimNames.Sub);

            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized("User ID not found in JWT token.");
            }
            var transactions = await _transactionRepository.GetTransactionsByUserId(int.Parse(userId));

            if (transactions == null)
            {
                return NotFound("No transactions found");
            }

            return Ok(transactions);
        }
        [HttpGet("getone/{id}")]

        [HttpPost("add")]
        public async Task<IActionResult> AddTransaction([FromBody] TransactionDTO transaction, int id)
        {
            //Get User Id from JWT tokens claims
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? User.FindFirstValue(JwtRegisteredClaimNames.Sub);

            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized("User ID not found in JWT token.");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var transactionEntity = new Transaction
            {
                Amount = transaction.Amount,
                CategoryId = transaction.CategoryId,
                Date = transaction.Date,
                Description = transaction.Description,
                Type = transaction.Type,
                UserId = int.Parse(userId)
            };
            var result =  await _transactionRepository.GetTransactionById(id);

            if (result == null)
            {
                return StatusCode(500, "Error saving transaction");
            }

            return Ok(result);
        }

        [HttpGet("getbyrange")]
        public async Task<IActionResult> GetTransactionsByRange([FromQuery] DateOnly startDate, [FromQuery] DateOnly endDate)
        {
            //Get User Id from JWT tokens claims
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? User.FindFirstValue(JwtRegisteredClaimNames.Sub);

            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized("User ID not found in JWT token.");
            }
            var transactions = await _transactionRepository.GetTransactionsByRange(int.Parse(userId), startDate, endDate);
            if (transactions == null)
            {
                return NotFound("No transactions found");
            }
            return Ok(transactions);
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateTransaction([FromBody] TransactionDTO transaction)
        {
            //Get User Id from JWT tokens claims
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? User.FindFirstValue(JwtRegisteredClaimNames.Sub);

            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized("User ID not found in JWT token.");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var transactionEntity = new Transaction
            {
                TransactionId = transaction.TransactionId,
                Amount = transaction.Amount,
                CategoryId = transaction.CategoryId,
                Date = transaction.Date,
                Description = transaction.Description,
                Type = transaction.Type,
                UserId = int.Parse(userId)
            };
            var result = await _transactionRepository.UpdateTransaction(transactionEntity);
            if (result == 0)
            {
                return StatusCode(500, "Error updating transaction");
            }
            return Ok(result);
        }

        [HttpDelete("delete/{transactionId}")]
        public async Task<IActionResult> DeleteTransaction(int transactionId)
        {
            //Get User Id from JWT tokens claims
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? User.FindFirstValue(JwtRegisteredClaimNames.Sub);

            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized("User ID not found in JWT token.");
            }
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }
            var result = await _transactionRepository.DeleteTransaction(transactionId);
            if (result == null)
            {
                return StatusCode(500, "Error deleting transaction");
            }
            return Ok(result);
        }
    }
}