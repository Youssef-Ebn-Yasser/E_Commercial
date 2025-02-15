namespace E_Commercial.ViewModel.CategoryTypeViewModel;

public class ShowAllCategoryTypeViewModel : CreateCategoryTypeViewModel
{
    public int? CategoryTypeId { get; set; }
    public DateTime CreatedDate { get; set; } = DateTime.Now;
    public int NumberOfCategory { get; set; }
}