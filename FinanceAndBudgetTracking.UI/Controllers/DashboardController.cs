using FinanceAndBudgetTracking.Models.DTO;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using FinanceAndBudgetTracking.UI.ViewModels;

namespace FinanceAndBudgetTracking.UI.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            var loggedUser = new UserDTO();
            var userJson = HttpContext.Session.GetString("AuthResponseUser");
            if (!string.IsNullOrEmpty(userJson))
            {
                loggedUser = JsonSerializer.Deserialize<UserDTO>(userJson);
                // Now you can access user.Name, user.Email, etc.
                //Do something with the user data
            }
            return View(loggedUser);
        }
    }
}
