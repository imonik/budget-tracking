using FinanceAndBudgetTracking.Models.DTO;

namespace FinanceAndBudgetTracking.UI.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<List<CategoryDTO>> GetCategoriesByUserAsync();
        Task<CategoryDTO?> GetCategoryByIdAsync(int id);
        Task<CategoryDTO> AddCategoryAsync(CategoryDTO category);
        Task<bool> UpdateCategoryAsync(CategoryDTO category);
        Task<bool> DeleteCategoryAsync(int id);
    }
}
