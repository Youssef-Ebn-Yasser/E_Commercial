namespace E_Commercial.Controllers;

public class ServicesController : Controller
{
    #region   Fields
    private readonly UserManager<User> _userManager;
    private User? _currentUser = new User();
    #endregion

    #region   Constructor
    public ServicesController(UserManager<User> userManager)
    {
        _userManager = userManager;
    }
    #endregion

    #region   Handle Methods
    public async Task<IActionResult> Index()
    {
        if (User.Identity is not null && User.Identity.IsAuthenticated)
        {
            _currentUser = await _userManager.GetUserAsync(User);
        }
        return View(_currentUser);
    }
    #endregion
}