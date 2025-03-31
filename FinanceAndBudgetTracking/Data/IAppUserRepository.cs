using FinanceAndBudgetTracking.DTO;
using FinanceAndBudgetTracking.Models;
using FinanceAndBudgetTracking.Services;

namespace FinanceAndBudgetTracking.Data
{
    public interface IAppUserRepository
    {
        Task<AppUser?> GetByEmailAsync(string email);
        Task<AppUser> RegisterAsync(AppUser user);
    }
}
