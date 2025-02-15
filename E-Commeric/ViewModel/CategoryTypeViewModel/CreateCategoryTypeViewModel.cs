namespace E_Commercial.ViewModel.CategoryTypeViewModel;

public class CreateCategoryTypeViewModel
{
    [Required(ErrorMessage = "Category Name is required")]
    public string? CategoryTypeName { get; set; }
}