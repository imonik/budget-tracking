using FinanceAndBudgetTracking.DataLayer.Entities;

namespace FinanceAndBudgetTracking.DataLayer.Interfaces
{
    public interface IAppUserRepository
    {
        Task<AppUser?> GetByEmailAsync(string email);
        Task<AppUser> RegisterAsync(AppUser user);
    }
}
