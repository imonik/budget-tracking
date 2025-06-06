﻿using FinanceAndBudgetTracking.Models;

namespace FinanceAndBudgetTracking.Data
{
    public interface IBudgetRepository
    {
        Task<Budget?> AddBudget(Budget budget);
        Task<Budget> GetBudgetById(int budgetId);
        Task<int> UpdateBudget(Budget budget);
        Task<Budget> DeleteBudget(int budgetId);
        Task<IEnumerable<Budget>> GetAllBudgetsByUser(int userId);
        Task<IEnumerable<Budget>> GetAllBudgetsByCategory(int categoryId);
    }
}
