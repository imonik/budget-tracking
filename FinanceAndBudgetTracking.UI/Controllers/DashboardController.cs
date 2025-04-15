using FinanceAndBudgetTracking.Models.DTO;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using FinanceAndBudgetTracking.UI.ViewModels;
using FinanceAndBudgetTracking.UI.Services.Interfaces;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace FinanceAndBudgetTracking.UI.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        //private readonly ILogger<DashboardController> _logger;
        private readonly IApiService _apiService;

        public DashboardController(IApiService apiService)
        {
            _apiService = apiService;
        }
        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirst("nameidentifier")?.Value;
            var email = User.FindFirst(ClaimTypes.Email)?.Value ?? User.FindFirst("email")?.Value;
            var role = User.FindFirst(ClaimTypes.Role)?.Value ?? User.FindFirst("role")?.Value;
            var daarboardView = new DashboardViewModel();
            var categoriesJson = await _apiService.GetAsync<List<CategoryDTO>>("categories/getallbyuser");
            var generalCategoriesJson = await _apiService.GetAsync<List<CategoryDTO>>("categories/getall");

            var loggedUser = new UserDTO();

            var userJson = HttpContext.Session.GetString("AuthResponseUser");
            if (!string.IsNullOrEmpty(userJson))
            {
                loggedUser = JsonSerializer.Deserialize<UserDTO>(userJson);

                //Do something with the user data
                daarboardView.User = loggedUser;
                daarboardView.CategoryList = categoriesJson;
                daarboardView.CategoryList = generalCategoriesJson;
            }
            return View(daarboardView);
        }
    }
}
