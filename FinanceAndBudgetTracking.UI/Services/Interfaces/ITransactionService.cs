using FinanceAndBudgetTracking.Models.DTO;

namespace FinanceAndBudgetTracking.UI.Services.Interfaces
{
    public interface ITransactionService
    {
        Task<List<TransactionDTO>> GetTransactionsByUserAsync();
        Task<TransactionDTO?> GetTransactionByIdAsync(int id);
        Task<bool> AddTransactionAsync(TransactionDTO transaction);
        Task<bool> UpdateTransactionAsync(TransactionDTO transaction);
        Task<bool> DeleteTransactionAsync(int id);
    }
}
