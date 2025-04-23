using System.Threading.Tasks;
using FinanceAndBudgetTracking.Models.DTO;
using FinanceAndBudgetTracking.UI.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FinanceAndBudgetTracking.UI.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        // GET: CategoryController
        public async Task<ActionResult> CategoryManagerAsync()
        {
            var categories = await _categoryService.GetCategoriesByUserAsync();
            return View(categories);
        }


        // GET: CategoryController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var category = await _categoryService.GetCategoryByIdAsync(1,id);
            if (category == null)
            {
                return NotFound(); // or RedirectToAction("Index");
            }
            return View(category);
        }

        // GET: CategoryController/Create
        public async Task<ActionResult> Create(UserCategoryDTO category)
        {
            await _categoryService.AddCategoryAsync(category);
            return View("CategoryManager");
        }

        // POST: CategoryController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(IFormCollection collection)
        {
            try
            {
                var newCategory = new UserCategoryDTO() { CategoryId = 0, Name = collection["CategoryName"] };
                var a  = await _categoryService.AddCategoryAsync(newCategory);

                return RedirectToAction(nameof(CategoryManagerAsync));
            }
            catch
            {
                return View();
            }
        }

        // GET: CategoryController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var category = await _categoryService.GetCategoryByIdAsync(1, id);
            return View(category);
        }

        // POST: CategoryController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, IFormCollection collection)
        {
            try
            {
                var newCategory = new UserCategoryDTO() { CategoryId = 0, Name = collection["CategoryName"] };
                var a = await _categoryService.AddCategoryAsync(newCategory);

                return RedirectToAction(nameof(CategoryManagerAsync));
            }
            catch
            {
                return View();
            }
        }

        // GET: CategoryController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            await _categoryService.DeleteCategoryAsync(id);
            return View();
        }

        // POST: CategoryController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, IFormCollection collection)
        {
            try
            {
                var deleted = await _categoryService.DeleteCategoryAsync(id);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
