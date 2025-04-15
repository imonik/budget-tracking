using FinanceAndBudgetTracking.Models.DTO;
using FinanceAndBudgetTracking.UI.Services.Interfaces;

namespace FinanceAndBudgetTracking.UI.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IApiService _apiService;

        public CategoryService(IApiService apiService)
        {
            _apiService = apiService;
        }
        public async Task<CategoryDTO> AddCategoryAsync(CategoryDTO category)
        {
            var response = await _apiService.PostAsync<CategoryDTO, CategoryDTO>("categories/add", category);
            return response;
        }

        public Task<bool> DeleteCategoryAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<CategoryDTO>> GetCategoriesByUserAsync()
        {
            throw new NotImplementedException();
        }

        public Task<CategoryDTO?> GetCategoryByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateCategoryAsync(CategoryDTO category)
        {
            throw new NotImplementedException();
        }
    }
}
