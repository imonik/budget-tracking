using FinanceAndBudgetTracking.Models;

namespace FinanceAndBudgetTracking.Data
{
    public interface ICategoryRepository
    {
        Task<UserCategory?> AddCategory(UserCategory category);
        Task<UserCategory> GetCategoryById(int categoryId);
        Task<int> UpdateCategory(UserCategory category);
        Task<UserCategory> DeleteCategory(int categmoryId);
        Task<IEnumerable<UserCategory>> GetAllCategoriesByUser(int userId);
        Task<IEnumerable<Category>> GetAllGeneralCategories();
    }
}
