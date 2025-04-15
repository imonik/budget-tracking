using FinanceAndBudgetTracking.Models.DTO;
using FinanceAndBudgetTracking.UI.Services.Interfaces;

namespace FinanceAndBudgetTracking.UI.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly IApiService _apiService;

        public TransactionService(IApiService apiService)
        {
            _apiService = apiService;
        }
        public async Task<bool> AddTransactionAsync(TransactionDTO transaction)
        {
            var response = await _apiService.PostAsync<TransactionDTO, TransactionDTO>("transactions", transaction);
            return response != null;
        }

        public async Task<bool> DeleteTransactionAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<TransactionDTO?> GetTransactionByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<TransactionDTO>> GetTransactionsByUserAsync()
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateTransactionAsync(TransactionDTO transaction)
        {
            throw new NotImplementedException();
        }
    }
}
