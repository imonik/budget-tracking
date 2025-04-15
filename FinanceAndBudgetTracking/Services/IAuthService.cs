using FinanceAndBudgetTracking.DataLayer.Entities;
using FinanceAndBudgetTracking.Models.DTO;

namespace FinanceAndBudgetTracking.API.Services
{
    public interface IAuthService
    {
        public void CreatePasswordHash(string password, out byte[] hash, out byte[] salt);
        public bool VerifyPasswordHash(string password, byte[] storedHash, byte[] stortedSalt);
        public bool IsValidPassword(string inputPassword, AppUser user);
        public LoginResponseDTO CreateLoginResponse(AppUser user, string token);
    }
}
