using Microsoft.AspNetCore.Mvc;

namespace FinanceAndBudgetTracking.UI.Controllers
{
    public class TransactionController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
