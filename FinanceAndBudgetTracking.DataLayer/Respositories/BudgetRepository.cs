using FinanceAndBudgetTracking.DataLayer.Entities;
using FinanceAndBudgetTracking.DataLayer.Interfaces;
using FinanceAndBudgetTracking.DataLayer.Services;
using Microsoft.EntityFrameworkCore;

namespace FinanceAndBudgetTracking.DataLayer.Repositories
{
    public class BudgetRepository : IBudgetRepository
    {
        private readonly AppDbContext _context;
        public BudgetRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<Budget?> AddBudget(Budget budget)
        {
            await _context.Budgets.AddAsync(budget);
            await _context.SaveChangesAsync();
            return budget;
        }
        public async Task<Budget> GetBudgetById(int budgetId)
        {
            return await _context.Budgets.FindAsync(budgetId);
        }
        public async Task<int> UpdateBudget(Budget budget)
        {
            _context.Budgets.Update(budget);
            return await _context.SaveChangesAsync();
        }
        public async Task<Budget> DeleteBudget(int budgetId)
        {
            var budget = await _context.Budgets.FindAsync(budgetId);
            _context.Budgets.Remove(budget);
            await _context.SaveChangesAsync();
            return budget;
        }
        public async Task<IEnumerable<Budget>> GetAllBudgetsByUser(int userId)
        {
            return await _context.Budgets.Where(b => b.UserId == userId).ToListAsync();
        }

        public async Task<IEnumerable<Budget>> GetAllBudgetsByCategory(int categoryId)
        {
            return await _context.Budgets.Where(b => b.CategoryId == categoryId).ToListAsync();
        }
    }
}
