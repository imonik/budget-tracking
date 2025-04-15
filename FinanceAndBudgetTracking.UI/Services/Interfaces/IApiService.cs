using FinanceAndBudgetTracking.Models.DTO;

namespace FinanceAndBudgetTracking.UI.Services.Interfaces
{
    public interface IApiService
    {
        Task<T?> GetAsync<T>(string endpoint);
        Task<TResponse?> PostAsync<TRequest, TResponse>(string endpoint, TRequest data);
        Task<HttpResponseMessage> DeleteAsync(string endpoint);
        Task<TResponse?> PutAsync<TRequest, TResponse>(string endpoint, TRequest data);
        //Task<T> PostAsync<T>(string v, CategoryDTO category);
    }
}