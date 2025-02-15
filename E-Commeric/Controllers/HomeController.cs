using DataLayer.UniteOfWorks; // Use System.Text.Json for better performance
using System.Text.Json;


namespace E_Commercia.Controllers;

public class HomeController : Controller
{
    #region   Fields
    private readonly IGenericInformation _genericInformation;
    private readonly IMapper _mapper;
    private readonly IProductHelper _productHelper;
    private readonly IProductService _productService;
    private readonly UserManager<User> _userManager;
    private readonly IUniteOfWork _uniteOfWork;
    private const string CookieKey = "Ids";
    private User? _currentUser = new User();
    #endregion

    #region   Constructor
    public HomeController(IGenericInformation genericInformation,
                          IMapper mapper,
                          IProductHelper productHelper,
                          UserManager<User> userManager,
                          IProductService productService,
                          IUniteOfWork uniteOfWork)
    {
        _genericInformation = genericInformation;
        _mapper = mapper;
        _productHelper = productHelper;
        _userManager = userManager;
        _productService = productService;
        _uniteOfWork = uniteOfWork;
    }
    #endregion

    #region   Handle Method
    public async Task<IActionResult> Index()
    {
        var result = await _productHelper.GetAllProductWithImages()!.ToListAsync();
        if (User.Identity is not null && User.Identity.IsAuthenticated)
        {
            _currentUser = await _userManager.GetUserAsync(User);
        }
        return View(result);
    }
    public async Task<IActionResult> ShowCategoryTypesAsync()
    {

        var result = await _genericInformation.CategoryTypesWithAllDependentAsync();
        var result2 = _mapper.Map<List<ShowCategoryTypeWithAllRelated>>(result);
        return View(result2);
    }
    public IActionResult ShowAboutPage()
    {

        return View();
    }
    public async Task<IActionResult> ShowCartPage()
    {

        // first get all ids in cookies 
        List<int?> ids = new();

        // Get existing cookie
        if (Request.Cookies.TryGetValue(CookieKey, out string? jsonString) && !string.IsNullOrEmpty(jsonString))
        {
            ids = JsonSerializer.Deserialize<List<int?>>(jsonString) ?? new List<int?>();
        }
        // get products mapping it and display it header
        var products = await _uniteOfWork.Repository<Product>()
                                                     .GetTableNoTracking()
                                                     .Where(p => ids.Contains(p.ProductId))
                                                     .Include(p => p.ProductImages)
                                                     .ToListAsync();
        // and show card details
        var productDtos = _mapper.Map<List<ProductDto>>(products);

        var cartView = _mapper.Map<CartPageViewModel>(productDtos);


        return View(cartView);
    }

    [HttpGet]
    public async Task<IActionResult> ShowProductDetails(int id)
    {
        var productDetails = await _productHelper.GetProductByIdAsyncWithImagesAsync(id);
        return View(productDetails);
    }
    [HttpGet]
    public IActionResult _SaveInCookiesAndRedirect(int productId)
    {
        List<int> ids = new();

        // Get existing cookie
        if (Request.Cookies.TryGetValue(CookieKey, out string? jsonString) && !string.IsNullOrEmpty(jsonString))
        {
            ids = JsonSerializer.Deserialize<List<int>>(jsonString) ?? new List<int>();
        }

        // Add new number to the list
        if (ids.Any(id => id == productId))
            return RedirectToAction(nameof(ShowCartPage));
        ids.Add(productId);


        // Save updated list back to cookie
        var options = new CookieOptions
        {
            Expires = DateTime.UtcNow.AddDays(30),
            HttpOnly = true, // Prevent JavaScript access
            Secure = true,   // Only send over HTTPS
            SameSite = SameSiteMode.Strict
        };

        Response.Cookies.Append(CookieKey, JsonSerializer.Serialize(ids), options);
        return RedirectToAction(nameof(ShowCartPage));
    }
    public IActionResult RemoveFromCookie(int productId)
    {
        if (Request.Cookies.TryGetValue(CookieKey, out string? jsonString) && !string.IsNullOrEmpty(jsonString))
        {
            var numbers = JsonSerializer.Deserialize<List<int>>(jsonString) ?? new List<int>();

            if (numbers.Remove(productId))
            {
                // Save updated list back to cookie
                var options = new CookieOptions
                {
                    Expires = DateTime.UtcNow.AddDays(7),
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.Strict
                };

                Response.Cookies.Append(CookieKey, JsonSerializer.Serialize(numbers), options);

                return RedirectToAction(nameof(ShowCartPage));
            }

            return NotFound(new { Message = $"Number {productId} not found." });
        }

        return NotFound(new { Message = "No list found in cookies." });
    }
    public IActionResult DeleteAll()
    {
        Response.Cookies.Delete(CookieKey);
        return RedirectToAction(nameof(ShowCartPage));
    }
    [HttpPost]
    public async Task<IActionResult> BuyAction()
    {
        var model = await _returnModel();
        // first get all ids from cookies
        List<int?> ids = new();

        // Get existing cookie
        if (Request.Cookies.TryGetValue(CookieKey, out string? jsonString) && !string.IsNullOrEmpty(jsonString))
        {
            ids = JsonSerializer.Deserialize<List<int?>>(jsonString) ?? new List<int?>();
        }

        // mapping to order
        var order = _mapper.Map<Order>(model);
        if (User.Identity is not null && User.Identity.IsAuthenticated)
        {
            _currentUser = await _userManager.GetUserAsync(User);
        }
        order.UserId = _currentUser.Id;
        try
        {
            // begin transaction
            _uniteOfWork.BeginTransaction();
            // save order
            await _uniteOfWork.Repository<Order>().AddAsync(order);
            _uniteOfWork.Complete();

            // save   productOrder


            // update Amount of product in stock
            var idList = model.productDto.Select(m => m.ProductId);
            foreach (var id in idList)
            {
                var product = await _uniteOfWork.Repository<Product>().GetByIdAsync(id);
                product.AmountInStock--;
                _uniteOfWork.Repository<Product>().Update(product);

                // save product Order
                var productOrder = new ProductOrder();

                productOrder.ProductId = id;
                productOrder.OrderId = order.OrderId;
                await _uniteOfWork.Repository<ProductOrder>().AddAsync(productOrder);
            }
            // success commit
            _uniteOfWork.Complete();
            _uniteOfWork.CommitTransaction();
            // then delete id from cookies
            Response.Cookies.Delete(CookieKey);
        }
        catch (Exception ex)
        {
            _uniteOfWork.RollbackTransaction();
            // failed rollback
        }
        // redirect to shop cart page
        return RedirectToAction(nameof(ShowCartPage));
    }

    private async Task<CartPageViewModel> _returnModel()
    {
        List<int?> ids = new();

        if (Request.Cookies.TryGetValue(CookieKey, out string? jsonString) && !string.IsNullOrEmpty(jsonString))
        {
            ids = JsonSerializer.Deserialize<List<int?>>(jsonString) ?? new List<int?>();
        }

        var products = await _uniteOfWork.Repository<Product>()
                                                     .GetTableNoTracking()
                                                     .Where(p => ids.Contains(p.ProductId))
                                                     .Include(p => p.ProductImages)
                                                     .ToListAsync();

        var productDtoList = _mapper.Map<List<ProductDto>>(products);

        var cartView = _mapper.Map<CartPageViewModel>(productDtoList);


        return cartView;
    }
    #endregion
}