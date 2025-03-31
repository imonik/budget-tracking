using FinanceAndBudgetTracking.Models;
using FinanceAndBudgetTracking.Services;
using Microsoft.EntityFrameworkCore;

namespace FinanceAndBudgetTracking.Data
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly AppDbContext _context;
        public CategoryRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<UserCategory?> AddCategory(UserCategory usercategory)
        {
            try
            {
                var newUserCategory =  _context.UserCategories.Add(usercategory);
                await _context.SaveChangesAsync();
                return newUserCategory.Entity;
            }
            catch (DbUpdateException dbEx)
            {
                // Log the exception or handle specific DB issues
                // e.g., _logger.LogError(dbEx, "Database update failed.");
                throw new ApplicationException("Could not save the new category to the database.", dbEx);
            }
            catch (Exception ex)
            {
                // Catch other unexpected exceptions
                throw new ApplicationException("An unexpected error occurred while adding the transaction.", ex);
            }
        }

        public async Task<UserCategory> DeleteCategory(int categoryId)
        {
            try
            {
                var existingCategory = await _context.UserCategories.FindAsync(categoryId);
                if (existingCategory == null)
                {
                    throw new ApplicationException("Category not found");
                }

                _context.UserCategories.Remove(existingCategory);
                await _context.SaveChangesAsync();
                return existingCategory;
            }
            catch (DbUpdateException dbEx)
            {
                // Log the exception or handle specific DB issues
                // e.g., _logger.LogError(dbEx, "Database update failed.");
                throw new ApplicationException("Could not save the new category to the database.", dbEx);
            }
            catch (Exception ex)
            {
                // Catch other unexpected exceptions
                throw new ApplicationException("An unexpected error occurred while adding the transaction.", ex);
            }
        }

        public async Task<IEnumerable<UserCategory>> GetAllCategoriesByUser(int userId)
        {
            try
            {
                return await _context.UserCategories.Where(c => c.UserId == userId).ToListAsync();

            }
            catch (Exception ex)
            {
                // Catch other unexpected exceptions
                throw new ApplicationException("An unexpected error occurred while retrieving the transaction.", ex);
            }
        }

        public async Task<IEnumerable<Category>> GetAllGeneralCategories()
        {
            try
            {
                var allCategories = await _context.Categories.ToListAsync();
                if (allCategories == null)
                {
                    throw new ApplicationException("Category not found");
                }
                return allCategories;
            }
            catch (Exception ex)
            {
                // Catch other unexpected exceptions
                throw new ApplicationException("An unexpected error occurred while retrieving the transaction.", ex);
            }
        }

        public async Task<UserCategory> GetCategoryById(int categoryId)
        {
            try
            {
                var userCategory = await _context.UserCategories.FindAsync(categoryId);
                if (userCategory == null)
                {
                    throw new ApplicationException("Category not found");
                }
                return userCategory;
            }
            catch (Exception ex)
            {
                // Catch other unexpected exceptions
                throw new ApplicationException("An unexpected error occurred while retrieving the transaction.", ex);
            }
        }

        public async Task<int> UpdateCategory(UserCategory category)
        {
            try
            {
                var userCategory = await _context.UserCategories.FindAsync(category.CategoryId);
                if (userCategory == null)
                {
                    throw new ApplicationException("Category not found");
                }
                //var updatedCategory = _context.UserCategories.Update(category);
                // ✅ Update properties manually
                userCategory.Name = category.Name;
                userCategory.ModifiedOn = DateTime.UtcNow;
                await _context.SaveChangesAsync();
                return userCategory.CategoryId;
            }
            catch (Exception ex)
            {
                // Catch other unexpected exceptions
                throw new ApplicationException("An unexpected error occurred while retrieving the transaction.", ex);
            }
        }
    }
}

