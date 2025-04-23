using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using FinanceAndBudgetTracking.DataLayer.Interfaces;
using FinanceAndBudgetTracking.DataLayer.Entities;
using FinanceAndBudgetTracking.Models.DTO;
using FinanceAndBudgetTracking.API.Services;
using FinanceAndBudgetTracking.API.Identity;


namespace FinanceAndBudgetTracking.Controllers
{

    [Authorize(Policy = IdentityData.AppUserPolicyName)]
    [ApiController]
    [Route("api/transaction")]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly ICurrentUserService _currentUser;
        public TransactionController(ITransactionRepository transactionRepository, ICurrentUserService currentUser)
        {
            _transactionRepository = transactionRepository;
            _currentUser = currentUser;
        }

        [HttpGet("getall")]
        public async Task<IActionResult> GetTransactions()
        {
            var userId = _currentUser.GetUserId();

            var transactions = await _transactionRepository.GetTransactionsByUserId(userId.Value);

            return Ok(transactions);
        }
        //[HttpGet("getone/{id}")]

        [HttpPost("add")]
        public async Task<IActionResult> AddTransaction([FromBody] TransactionDTO transaction, int id)
        {
            var userId = _currentUser.GetUserId();


            var transactionEntity = new Transaction
            {
                Amount = transaction.Amount,
                CategoryId = transaction.CategoryId,
                Date = transaction.Date,
                Description = transaction.Description,
                Type = transaction.Type,
                UserId = userId.Value
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
            var userId = _currentUser.GetUserId();

            var transactions = await _transactionRepository.GetTransactionsByRange(userId.Value, startDate, endDate);
            if (transactions == null)
            {
                return NotFound("No transactions found");
            }
            return Ok(transactions);
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateTransaction([FromBody] TransactionDTO transaction)
        {
            var userId = _currentUser.GetUserId();

            var transactionEntity = new Transaction
            {
                TransactionId = transaction.TransactionId,
                Amount = transaction.Amount,
                CategoryId = transaction.CategoryId,
                Date = transaction.Date,
                Description = transaction.Description,
                Type = transaction.Type,
                UserId = userId.Value
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
            var userId = _currentUser.GetUserId();

            var result = await _transactionRepository.DeleteTransaction(transactionId);
            if (result == null)
            {
                return StatusCode(500, "Error deleting transaction");
            }
            return Ok(result);
        }
    }
}