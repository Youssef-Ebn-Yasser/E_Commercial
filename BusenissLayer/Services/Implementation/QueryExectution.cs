namespace BusinessLayer.Services.Implementation;

public class QueryExecution : IQueryExecution
{
    #region   Fields
    private readonly ApplicationDbContext _context;
    #endregion

    #region   Constructor
    public QueryExecution(ApplicationDbContext context)
    {
        _context = context;
    }
    #endregion

    #region   Handle Methods
    public IEnumerable<TResult> ExecuteQuery<TResult>(string sqlQuery, params object[] parameters) where TResult : class
    {
        return _context.Database.SqlQueryRaw<TResult>(sqlQuery, parameters).ToList();
    }
    #endregion
}