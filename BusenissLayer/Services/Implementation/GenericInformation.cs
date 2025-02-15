namespace BusinessLayer.Services.Implementation;

public class GenericInformation : IGenericInformation
{
    #region   Fields
    private readonly IUniteOfWork _uniteOfWork;
    #endregion

    #region   Constructor
    public GenericInformation(IUniteOfWork uniteOfWork)
    {
        _uniteOfWork = uniteOfWork;
    }
    #endregion

    #region   Handle Methods
    public async Task<List<CategoryType>> CategoryTypesWithAllDependentAsync()
    {
        var result = await _uniteOfWork.Repository<CategoryType>()
                                                      .GetTableAsTracking()
                                                      .Include(c => c.Categories)!
                                                      .ThenInclude(ct => ct.products)!
                                                      .ThenInclude(p => p.ProductImages).ToListAsync();

        return result;
    }
    #endregion
}