using Microsoft.AspNetCore.Mvc.Rendering;

namespace E_Commercial.Controllers
{
    public class ProductController : Controller
    {
        #region   Fields
        private readonly ICategoryHelper _categoryHelper;
        private readonly IProductHelper _productHelper;

        #endregion

        #region   Constructor
        public ProductController(ICategoryHelper categoryHelper,
                                 IProductHelper productHelper)
        {
            _categoryHelper = categoryHelper;
            _productHelper = productHelper;
        }

        #endregion
        #region   HandelMethods
        [HttpGet]
        public async Task<IActionResult> ShowAll(string search)
        {
            if (ModelState.IsValid)
            {
                var filteredProducts = await _productHelper.GetProductsContainsAsync(search);
                ViewBag.Search = search;
                return View(filteredProducts);
            }

            var result = await _productHelper.GetAllProductsWithoutImagesAsync();
            return View(result);
        }
        [HttpGet]
        public IActionResult Create()
        {
            ViewData["categories"] = getCategories();

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateProductViewModel createProductViewModel)
        {

            var result = await _productHelper.CreateAsync(createProductViewModel);

            if (result)
                return RedirectToAction(nameof(ShowAll));

            ViewData["categories"] = getCategories();

            return View();
        }


        public async Task<IActionResult> Delete(int id)
        {

            var result = await _productHelper.DeleteAsync(id);

            if (result)
                return RedirectToAction(nameof(ShowAll));

            ModelState.AddModelError("", "error happen can not delete this product");
            return RedirectToAction(nameof(ShowAll));

        }

        public async Task<IActionResult> Update(int id)
        {
            var result = await _productHelper.FindProductWithImagesAsync(id);

            ViewData["categories"] = getCategories();

            return View(result);
        }
        [HttpPost]
        public async Task<IActionResult> Update(UpdateProductViewModel updateProductViewModel)
        {
            if (!ModelState.IsValid)
            {
                ViewData["categories"] = getCategories();
                ModelState.AddModelError("", "error happen can not delete this product");
                return View();
            }
            var result = await _productHelper.UpdateAsync(updateProductViewModel);

            if (result)
                return RedirectToAction(nameof(ShowAll));

            ViewData["categories"] = getCategories();
            ModelState.AddModelError("", "error happen can not delete this product");
            return View();
        }
        private SelectList? getCategories()
        {
            var categories = _categoryHelper.GetAllCategoryWithoutProducts();

            return categories is not null ? new SelectList(categories.ToList(), "CategoryId", "CategoryName") : null;
        }
        #endregion
    }
}