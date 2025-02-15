namespace E_Commercial.ViewModel.CategoryViewModel;

public class CreateCategoryViewModel
{
    [Required(ErrorMessage = "Category Name is required")]
    public string? CategoryName { get; set; } = string.Empty;
    public int CategoryTypeId { get; set; }
}