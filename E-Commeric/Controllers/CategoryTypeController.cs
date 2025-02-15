namespace E_Commercial.Controllers;

public class CategoryTypeController : Controller
{
    #region   Fields
    private readonly ICategoryTypeHelper _categoryTypeHelper;
    #endregion

    #region   Constrictor
    public CategoryTypeController(ICategoryTypeHelper categoryTypeHelper)
    {
        _categoryTypeHelper = categoryTypeHelper;
    }
    #endregion

    #region   Handle Methods
    [HttpGet]
    public async Task<IActionResult> ShowAll(string search)
    {
        if (ModelState.IsValid)
        {
            var filteredProducts = await _categoryTypeHelper.GetCategoryTypeContainsAsync(search);
            ViewBag.Search = search;
            return View(filteredProducts);
        }

        var result = _categoryTypeHelper.GetAllCategoryTypeWithCategories();
        if (result is not null)
            return View(await result.ToListAsync());

        return View(result);
    }
    [HttpGet]
    public async Task<IActionResult> Update(int id)
    {
        var result = await _categoryTypeHelper.FindCategoryTypeAsync(id);

        if (result is not null)
            return View(result);

        ModelState.AddModelError("", "there is an error no categoryType Exist");
        return RedirectToAction(nameof(ShowAll));
    }
    [HttpPost]
    public async Task<IActionResult> Update(UpdateCategoryTypeViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        var success = await _categoryTypeHelper.UpdateAsync(model);

        if (!success)
            return View(model);

        return RedirectToAction(nameof(ShowAll));
    }
    public async Task<IActionResult> Delete(int? id)
    {
        var success = await _categoryTypeHelper.DeleteAsync(id);

        if (!success)
        {
            ModelState.AddModelError("", "there is an error no categoryType Exist");
            return View();
        }

        return RedirectToAction(nameof(ShowAll));
    }
    [HttpGet]
    public IActionResult Create() => View();
    [HttpPost]
    public async Task<IActionResult> Create(CreateCategoryTypeViewModel model)
    {
        var success = await _categoryTypeHelper.CreateAsync(model);

        if (!success)
            return View(model);

        return RedirectToAction(nameof(ShowAll));
    }
    #endregion
}