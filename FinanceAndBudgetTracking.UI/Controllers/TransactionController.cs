using FinanceAndBudgetTracking.Models.DTO;
using FinanceAndBudgetTracking.UI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FinanceAndBudgetTracking.UI.Controllers
{
    public class TransactionController : Controller
    {
        private readonly ITransactionService _transactionService;
        public TransactionController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }
        public async Task<IActionResult> TransactionManager()
        {
            var transactions = await _transactionService.GetTransactionsByUserAsync();
            
            return View(transactions);
        }

        public async Task<IActionResult> Create(TransactionDTO transaction)
        {
            await _transactionService.AddTransactionAsync(transaction);
            return View(nameof(TransactionManager));
        }
    }
}
