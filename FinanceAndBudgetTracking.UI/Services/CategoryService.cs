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
        public async Task<UserCategoryDTO> AddCategoryAsync(UserCategoryDTO category)
        {
            var response = await _apiService.PostAsync<UserCategoryDTO, UserCategoryDTO>("categories/add", category);
            return response;
        }

        public Task<bool> DeleteCategoryAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<UserCategoryDTO>> GetCategoriesByUserAsync()
        {
            var response = await _apiService.GetAsync<List<UserCategoryDTO>>("categories/getallbyuser");
            return response;
        }

        public async Task<UserCategoryDTO?> GetCategoryByIdAsync(int userId, int categoryId)
        {
            var response = await _apiService.GetByIdAsync<UserCategoryDTO>("categories/detail", categoryId);
            return response;
        }

        public async Task<int> UpdateCategoryAsync(UserCategoryDTO category)
        {
            var response = await _apiService.PutAsync<UserCategoryDTO, int>("categories/updatecategory", category);//GetByIdAsync<UserCategoryDTO>("categories/detail", categoryId);
            return response;
        }
    }
}
