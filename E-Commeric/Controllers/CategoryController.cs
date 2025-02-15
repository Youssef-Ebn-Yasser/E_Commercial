using Microsoft.AspNetCore.Mvc.Rendering;

namespace E_Commercial.Controllers;

public class CategoryController : Controller
{
    #region   Fields
    private readonly ICategoryTypeHelper _categoryTypeHelper;
    private readonly ICategoryHelper _categoryHelper;
    #endregion

    #region   Constrictor
    public CategoryController(ICategoryTypeHelper categoryTypeHelper, ICategoryHelper categoryHelper)
    {
        _categoryTypeHelper = categoryTypeHelper;
        _categoryHelper = categoryHelper;
    }
    #endregion

    #region   Handle Methods
    [HttpGet]
    public async Task<IActionResult> ShowAll(string search)
    {
        if (ModelState.IsValid)
        {
            var filteredProducts = await _categoryHelper.GetCategoryContainsAsync(search);
            ViewBag.Search = search;
            return View(filteredProducts);
        }
        var result = _categoryHelper.GetShowAllViewModelList();

        return View(result);
    }
    [HttpGet]
    public async Task<IActionResult> Update(int id)
    {
        var result = await _categoryHelper.FindCategoryAsync(id);
        var categoryTypes = _categoryTypeHelper.GetAllCategoryTypeWithoutCategories();
        if (categoryTypes is not null)
            ViewData["categories"] = new SelectList(await categoryTypes.ToListAsync(), "CategoryTypeId", "CategoryTypeName");

        if (result is not null)
            return View(result);

        ModelState.AddModelError("", "there is an error no categoryType Exist");
        return RedirectToAction(nameof(ShowAll));
    }
    [HttpPost]
    public async Task<IActionResult> Update(UpdateCategoryViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        var success = await _categoryHelper.UpdateAsync(model);

        if (success)
            return RedirectToAction(nameof(ShowAll));

        var categoryTypes = _categoryTypeHelper.GetAllCategoryTypeWithoutCategories();
        if (categoryTypes is not null)
            ViewData["categories"] = new SelectList(await categoryTypes.ToListAsync(), "CategoryTypeId", "CategoryTypeName");
        return View(model);

    }
    public async Task<IActionResult> Delete(int? id)
    {
        var success = await _categoryHelper.DeleteAsync(id);

        if (!success)
        {
            ModelState.AddModelError("", "there is an error no categoryType Exist");
            return View();
        }

        return RedirectToAction(nameof(ShowAll));
    }
    [HttpGet]
    public async Task<IActionResult> Create()
    {
        var categoryTypes = _categoryTypeHelper.GetAllCategoryTypeWithoutCategories();
        if (categoryTypes is not null)
            ViewData["categories"] = new SelectList(await categoryTypes.ToListAsync(), "CategoryTypeId", "CategoryTypeName");

        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Create(CreateCategoryViewModel model)
    {
        var success = await _categoryHelper.CreateAsync(model);

        if (!success)
            return View(model);

        return RedirectToAction(nameof(ShowAll));
    }
    #endregion
}