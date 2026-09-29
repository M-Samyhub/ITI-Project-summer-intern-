using ItiFinalProject.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ItiFinalProject.View_Components
{
    public class CategoryDropdownViewComponent : ViewComponent
    {
        private readonly ICategoryService _categoryService;
        public CategoryDropdownViewComponent(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task<IViewComponentResult> InvokeAsync(int? selectedValue = null, string name = "CategoryId")
        {
            var categories = await _categoryService.GetAllAsync();

            var selectList = new SelectList(categories, "Id", "Name", selectedValue);

            ViewBag.InputName = name;
            ViewBag.SelectedValue = selectedValue;
            return View(selectList);
        }
    }
}
