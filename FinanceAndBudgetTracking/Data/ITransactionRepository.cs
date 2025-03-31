using FinanceAndBudgetTracking.Models;

namespace FinanceAndBudgetTracking.Data
{
    public interface ITransactionRepository
    {
        Task<Transaction?> AddTransaction(Transaction transaction);
        Task<Transaction> GetTransactionById(int transactionId);
        Task<int> UpdateTransaction(Transaction transaction);
        Task<Transaction> DeleteTransaction(int transactionId);
        Task<IEnumerable<Transaction>> GetTransactionsByUserId(int userId);
        Task<IEnumerable<Transaction>> GetTransactionsByRange(int id,DateOnly startDate, DateOnly endDate);
        Task<IEnumerable<Transaction>> GetTransactionsByType(bool type);
    }
}
