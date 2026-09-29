using ItiFinalProject.Interfaces.Services;
using ItiFinalProject.View_Model.Category;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ItiFinalProject.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var Categories = await _categoryService.GetAllAsync();
            return View(Categories);
        }
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var category = await _categoryService.GetByIdAsync(id);
            return View(category);
        }

        [HttpGet]
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create(CreateCategoryViewModel model)
        {
            if(!ModelState.IsValid)
                return View(model);

            await _categoryService.CreateAsync(model);
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Edit(int id) 
        {
            var category = await _categoryService.GetForEditByIdAsync(id);
            if (category == null)
                return NotFound();

            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(int id ,UpdateCategoryViewModel model) 
        {
            if (id != model.Id) 
                return BadRequest();

            if (!ModelState.IsValid)
                return View(model);

            await _categoryService.UpdateAsync(id, model);
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            var category = await _categoryService.GetByIdAsync(id);
            if (category == null) 
                return NotFound();

            await _categoryService.DeleteAsync(id);
            return RedirectToAction("Index");
        }
    }
}
