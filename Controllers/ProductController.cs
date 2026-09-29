using ItiFinalProject.Interfaces.Services;
using ItiFinalProject.View_Model.Product;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ItiFinalProject.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var products = await _productService.GetAllAsync();
            return View(products);
        }
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var product = await _productService.GetByIdAsync(id);
            return View(product);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateProductViewModel model)
        {
            if (model.CategoryId == null || model.CategoryId <= 0)
                ModelState.AddModelError("CategoryId", "Category is required");
            
            if (!ModelState.IsValid)
                return View(model);
            await _productService.CreateAsync(model);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var product = await _productService.GetForEditByIdAsync(id);
            if (product == null)
                return NotFound();

            return View(product);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,UpdateProductViewModel model)
        {
            if (id != model.Id)
                return BadRequest();

            if (model.CategoryId == null || model.CategoryId <= 0)
                ModelState.AddModelError("CategoryId", "Category is required");

            if (!ModelState.IsValid)
                return View(model);

            await _productService.UpdateAsync(id , model);
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _productService.GetByIdAsync(id);
            if(product == null)
                return NotFound();

            await _productService.DeleteAsync(id);
            return RedirectToAction("Index");
        }



    }
}
