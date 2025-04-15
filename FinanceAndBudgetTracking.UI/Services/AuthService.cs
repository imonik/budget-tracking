using System.Linq;
using System.Reflection;
using System.Text.Json;
using FinanceAndBudgetTracking.Models;
using FinanceAndBudgetTracking.Models.DTO;
using FinanceAndBudgetTracking.UI.Services.Interfaces;
using FinanceAndBudgetTracking.UI.ViewModels;

namespace FinanceAndBudgetTracking.UI.Services
{
    public class AuthService : IAuthService
    {
        private readonly IApiService _apiService;
        public AuthService(IApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task<LoginResponseDTO> LoginAsync(LoginRequestDTO login)
        {
            return await _apiService.PostAsync<LoginRequestDTO, LoginResponseDTO>("auth/login", login);
        }

        public Task<bool> LogoutAsync()
        {
            throw new NotImplementedException();
        }

        public async  Task<bool> RegisterAsync(RegisterDTO register)
        {
            //var response = await _httpClient.PostAsJsonAsync("auth/register", register);
            //if (response.IsSuccessStatusCode) 
            //{


            //}
            //return response.IsSuccessStatusCode;
            return true;
        }
        // Implement methods for authentication and authorization here
    }
}
