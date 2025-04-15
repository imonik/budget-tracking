using System.Text.Json;
using FinanceAndBudgetTracking.Models.DTO;
using FinanceAndBudgetTracking.UI.Services.Interfaces;
using FinanceAndBudgetTracking.UI.ViewModels;

namespace FinanceAndBudgetTracking.UI.Services
{
    public class DashboardService : IDashboardService
    {
        private readonly IApiService _apiService;
        public DashboardService(IApiService apiService)
        {
            _apiService = apiService;
        }
        public async Task<DashboardViewModel> BuildDashboardAsync(HttpContext context)
        {
            var dashboard = new DashboardViewModel();

            var userJson = context.Session.GetString("AuthResponseUser");

            if (!string.IsNullOrEmpty(userJson))
            {
                dashboard.User = JsonSerializer.Deserialize<UserDTO>(userJson);
            }
            dashboard.CategoryList = await _apiService.GetAsync<List<CategoryDTO>>("categories/getall");            
            
            return dashboard;
        }
    }
}
