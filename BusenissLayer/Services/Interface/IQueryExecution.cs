namespace BusinessLayer.Services.Interface;

public interface IQueryExecution
{
    public IEnumerable<TResult> ExecuteQuery<TResult>(string sqlQuery, params object[] parameters) where TResult : class;
}