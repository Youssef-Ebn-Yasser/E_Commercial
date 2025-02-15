namespace E_Commercial.Controllers;

public class AccountController : Controller
{
    #region   Fields
    private readonly IAccountHelper _accountHelper;
    #endregion

    #region   Constructor
    public AccountController(IAccountHelper accountHelper)
    {
        _accountHelper = accountHelper;
    }
    #endregion

    #region   Handle Methods
    [HttpGet]
    public IActionResult Login(string returnUrl)
    {
        var loginViewModel = new LoginViewModel()
        {
            ReturnUrl = returnUrl
        };

        return View(loginViewModel);
    }
    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        var result = await _accountHelper.LoginAsync(model);

        if (!result)
        {
            ModelState.AddModelError(string.Empty, "UserName Or Password is Wrong");
            return View(model);
        }

        if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
        {
            return LocalRedirect(model.ReturnUrl);
        }

        return RedirectToAction("Index", "Home");
    }


    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        var result = await _accountHelper.RegisterAsync(model);

        if (!result)
            return View(model);

        return RedirectToAction("index", "Home");
    }

    [HttpGet]
    public async Task<IActionResult> LogOut()
    {
        await _accountHelper.LogOutAsync();

        return RedirectToAction("Index", "Home");
    }
    #endregion
}