using FinanceAndBudgetTracking.DTO;
using FinanceAndBudgetTracking.Models;
using FinanceAndBudgetTracking.Services;
using Microsoft.EntityFrameworkCore;

namespace FinanceAndBudgetTracking.Data
{
    public class AppUserRepository: IAppUserRepository
    {
        private readonly AppDbContext _context;
        public AppUserRepository(AppDbContext appDbContext) 
        {
            _context = appDbContext;
        }
        public async Task<AppUser?> GetByEmailAsync(string email)
        {
            return await _context.AppUsers.FirstOrDefaultAsync(u => u.Email == email);
        }
        public async Task<AppUser> RegisterAsync(AppUser user)
        {
            await _context.AppUsers.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;
        }
    }
}
