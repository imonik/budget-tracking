using FinanceAndBudgetTracking.Models.DTO;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using FinanceAndBudgetTracking.UI.ViewModels;
using FinanceAndBudgetTracking.UI.Services.Interfaces;

namespace FinanceAndBudgetTracking.UI.Controllers
{
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
            var daarboardView = new DashboardViewModel();
            var categoriesJson = await _apiService.GetAsync<List<CategoryDTO>>("categories/getallbyuser");
            var generalCategoriesJson = await _apiService.GetAsync<List<CategoryDTO>>("categories/getall");

            var loggedUser = new UserDTO();

            var userJson = HttpContext.Session.GetString("AuthResponseUser");
            if (!string.IsNullOrEmpty(userJson))
            {
                loggedUser = JsonSerializer.Deserialize<UserDTO>(userJson);

                // Now you can access user.Name, user.Email, etc.
                //Do something with the user data
                daarboardView.User = loggedUser;
                daarboardView.CategoryList = categoriesJson;
                daarboardView.CategoryList = generalCategoriesJson;
            }


            return View(daarboardView);
        }
    }
}
