using FinanceAndBudgetTracking.DataLayer.Entities;

namespace FinanceAndBudgetTracking.API.Services
{
    public interface IJwtService
    {
        public string GenerateToken(AppUser user);
    }
}
