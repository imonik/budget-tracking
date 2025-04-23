using FinanceAndBudgetTracking.Models.DTO;

namespace FinanceAndBudgetTracking.UI.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<List<UserCategoryDTO>> GetCategoriesByUserAsync();
        Task<UserCategoryDTO> GetCategoryByIdAsync(int userId, int categoryId);
        Task<UserCategoryDTO> AddCategoryAsync(UserCategoryDTO category);
        Task<int> UpdateCategoryAsync(UserCategoryDTO category);
        Task<bool> DeleteCategoryAsync(int id);
    }
}
