using FinanceAndBudgetTracking.Models.DTO;
using FinanceAndBudgetTracking.UI.ViewModels;

namespace FinanceAndBudgetTracking.UI.Services.Interfaces
{
    public interface ITransactionService
    {
        Task<TransactionViewModel> GetTransactionsByUserAsync();
        Task<TransactionDTO?> GetTransactionByIdAsync(int id);
        Task<TransactionDTO> AddTransactionAsync(TransactionDTO transaction);
        Task<bool> UpdateTransactionAsync(TransactionDTO transaction);
        Task<bool> DeleteTransactionAsync(int id);
    }
}
