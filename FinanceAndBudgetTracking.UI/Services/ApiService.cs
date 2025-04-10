using System.Net.Http.Headers;
using FinanceAndBudgetTracking.UI.Services.Interfaces;

namespace FinanceAndBudgetTracking.UI.Services
{
    public class ApiService : IApiService
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public ApiService(HttpClient httpClient, IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = httpClient;
            _httpContextAccessor = httpContextAccessor;
        }

        public Task<HttpResponseMessage> DeleteAsync(string endpoint)
        {
            throw new NotImplementedException();
        }

        public async Task<T?> GetAsync<T>(string endpoint)
        {
            var token = _httpContextAccessor.HttpContext?.Session.GetString("JWT");

            if (!string.IsNullOrEmpty(token))
            {
                _httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", token);
            }

            var response = await _httpClient.GetAsync(endpoint);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<T>();
            }

            return default;
        }

        public Task<TResponse?> PostAsync<TRequest, TResponse>(string endpoint, TRequest data)
        {
            throw new NotImplementedException();
        }

        public Task<TResponse?> PutAsync<TRequest, TResponse>(string endpoint, TRequest data)
        {
            throw new NotImplementedException();
        }
    }
}
