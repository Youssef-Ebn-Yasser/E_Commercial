namespace BusinessLayer.Services.Interface;

public interface IGenericInformation
{
    public Task<List<CategoryType>> CategoryTypesWithAllDependentAsync();

}