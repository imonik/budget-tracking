using FinanceAndBudgetTracking.API.Services;
using FinanceAndBudgetTracking.DataLayer.Entities;
using FinanceAndBudgetTracking.Models.DTO;

namespace FinanceAndBudgetTracking.Services
{
    public class AuthService : IAuthService
    {
        public AuthService() { }

        public void CreatePasswordHash(string password, out byte[] hash,  out byte[] salt)
        {
            using var hmac = new System.Security.Cryptography.HMACSHA512();
            salt = hmac.Key;
            hash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        }

        public bool VerifyPasswordHash(string password, byte[] storedHash, byte[] stortedSalt)
        {
            using var hmac = new System.Security.Cryptography.HMACSHA512(stortedSalt);
            var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            for (int i = 0; i < computedHash.Length; i++)
            {
                if (computedHash[i] != storedHash[i]) return false;
            }
            return true;
        }
        public bool IsValidPassword(string inputPassword, AppUser user)
        {
            return VerifyPasswordHash(inputPassword, user.PasswordHash, user.SaltHash);
        }

        public LoginResponseDTO CreateLoginResponse(AppUser user, string token)
        {
            return new LoginResponseDTO
            {
                Token = token,
                User = new UserDTO
                {
                    AppUserId = user.AppUserId,
                    Name = user.Name,
                    Email = user.Email, 
                    Success = true
                }
            };
        }
    }
}
