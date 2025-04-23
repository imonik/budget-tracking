using FinanceAndBudgetTracking.Models.DTO;
using FinanceAndBudgetTracking.UI.Services.Interfaces;
using FinanceAndBudgetTracking.UI.ViewModels;

namespace FinanceAndBudgetTracking.UI.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly IApiService _apiService;

        public TransactionService(IApiService apiService)
        {
            _apiService = apiService;
        }
        public async Task<TransactionDTO> AddTransactionAsync(TransactionDTO transaction)
        {
            var response = await _apiService.PostAsync<TransactionDTO, TransactionDTO>("transactions/getall", transaction);
            return response;
        }

        public async Task<bool> DeleteTransactionAsync(int id)
        {
            throw new NotImplementedException();
        }
         
        public Task<TransactionDTO?> GetTransactionByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<TransactionViewModel> GetTransactionsByUserAsync()
        {
            var categoryList = await _apiService.GetAsync<List<CategoryDTO>>("categories/getallbyuser");
            var transactionList = await _apiService.GetAsync<List<TransactionDTO>>("transaction/getall");
            var transactionViewModel = new TransactionViewModel
            {
                Transactions = transactionList,
                CategoryList = categoryList
            };
            return transactionViewModel;
        }

        public async Task<bool> UpdateTransactionAsync(TransactionDTO transaction)
        {
            //var response = await _apiService.PostAsync<TransactionDTO, TransactionDTO>("transactions", transaction);
            return true;
        }
    }
}
