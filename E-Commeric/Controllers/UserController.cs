using E_Commercial.ViewModel.Identity.User;
using Microsoft.EntityFrameworkCore;

namespace E_Commercial.Controllers;

public class UserController : Controller
{
    #region   Fields
    private readonly IUserHelper _userHelper;
    #endregion

    #region   Constructor
    public UserController(IUserHelper userHelper)
    {
        _userHelper = userHelper;
    }
    #endregion

    #region   Handle Methods
    public async Task<IActionResult> ShowAll(string search)
    {
        if (ModelState.IsValid)
        {
            var filteredProducts = await _userHelper.GetUserContains(search);
            ViewBag.Search = search;
            return View(filteredProducts);
        }

        var showUsersViewModel = _userHelper.GetAllUsersExceptClient();

        if (showUsersViewModel == null)
            return View(showUsersViewModel);

        var showUsersViewModelList = await showUsersViewModel.ToListAsync();
        return View(showUsersViewModelList);
    }

    [HttpGet]
    public IActionResult Create() => View();

    [HttpPost]
    public async Task<IActionResult> Create(CreateUserViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        var result = await _userHelper.CreateAsync(model);

        if (result)
            return RedirectToAction(nameof(ShowAll));

        ModelState.AddModelError("", "there is an error in the data you enter");
        return View(model);
    }

    [HttpGet]
    public async Task<IActionResult> Update(string id)
    {
        var userExist = await _userHelper.FindUserAsync(id);

        if (userExist == null)
            return NotFound();

        return View(userExist);
    }

    [HttpPost]
    public async Task<IActionResult> Update(UpdateUserViewModel model)
    {
        if (!ModelState.IsValid)
        {
            ModelState.AddModelError("", "there is an error in the data you enter");
            return View(model);
        }

        var results = await _userHelper.UpdateAsync(model);

        if (results)
            return RedirectToAction(nameof(ShowAll));

        ModelState.AddModelError("", "there is an error after go ");
        return View(model);
    }

    public async Task<IActionResult> Delete(string id)
    {
        if (!ModelState.IsValid)
        {
            ModelState.AddModelError("", "there is an error in the data you enter");
            return RedirectToAction(nameof(ShowAll));
        }

        var results = await _userHelper.DeleteAsync(id);

        if (results)
            return RedirectToAction(nameof(ShowAll));

        ModelState.AddModelError("", "there is an error after go ");
        return RedirectToAction(nameof(ShowAll));
    }
    #endregion
}