using System.Linq;
using System.Text.Json;
using FinanceAndBudgetTracking.Models;
using FinanceAndBudgetTracking.Models.DTO;
using FinanceAndBudgetTracking.UI.Services.Interfaces;
using FinanceAndBudgetTracking.UI.ViewModels;

namespace FinanceAndBudgetTracking.UI.Services
{
    public class AuthService : IAuthService
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public AuthService(HttpClient httpClient, IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = httpClient;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<UserDTO> LoginAsync(LoginRequestDTO login)
        {
            var response = await _httpClient.PostAsJsonAsync("auth/login", login);
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();

                var tokenResponse = JsonSerializer.Deserialize<LoginResponseDTO>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                if (tokenResponse != null)
                {
                    tokenResponse.User.Success = true;
                    _httpContextAccessor.HttpContext?.Session.SetString("JWT", tokenResponse.Token);
                    _httpContextAccessor.HttpContext.Session.SetString("AuthResponseUser", JsonSerializer.Serialize(tokenResponse.User));
                    return tokenResponse.User;
                }
            }
            return new UserDTO { Success = false };
        }

        public Task<bool> LogoutAsync()
        {
            throw new NotImplementedException();
        }

        public async  Task<bool> RegisterAsync(RegisterDTO register)
        {
            var response = await _httpClient.PostAsJsonAsync("auth/register", register);
            if (response.IsSuccessStatusCode) 
            {
                
            
            }
            
            return response.IsSuccessStatusCode;
        }
        // Implement methods for authentication and authorization here
    }
}
