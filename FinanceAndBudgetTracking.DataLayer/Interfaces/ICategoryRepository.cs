using FinanceAndBudgetTracking.DataLayer.Entities;

namespace FinanceAndBudgetTracking.DataLayer.Interfaces
{
    public interface ICategoryRepository
    {
        Task<UserCategoryDTO?> AddCategory(UserCategoryDTO category);
        Task<UserCategoryDTO> GetCategoryById(int userId, int categoryId);
        Task<int> UpdateCategory(UserCategoryDTO category);
        Task<UserCategoryDTO> DeleteCategory(int categmoryId);
        Task<IEnumerable<UserCategoryDTO>> GetAllCategoriesByUser(int userId);
        Task<IEnumerable<Category>> GetAllGeneralCategories();
    }
}
