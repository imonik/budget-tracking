using Microsoft.AspNetCore.Mvc;
using FinanceAndBudgetTracking.UI.Services.Interfaces;

namespace FinanceAndBudgetTracking.UI.Controllers
{
    
    public class DashboardController : Controller
    {
        //private readonly ILogger<DashboardController> _logger;
        private readonly IDashboardService _dashService;

        public DashboardController(IDashboardService dashService)
        {
            _dashService = dashService;
        }
        public async Task<IActionResult> Index()
        {
            var viewModel = await _dashService.BuildDashboardAsync(HttpContext);
                
            return View(viewModel);
        }
    }
}
