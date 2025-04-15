using FinanceAndBudgetTracking.UI.ViewModels;

namespace FinanceAndBudgetTracking.UI.Services.Interfaces
{
    public interface IDashboardService
    {
        Task<DashboardViewModel> BuildDashboardAsync(HttpContext context);
    }
}
