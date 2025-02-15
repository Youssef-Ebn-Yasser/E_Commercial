namespace DataLayer.UniteOfWorks;

public interface IUniteOfWork : IDisposable
{
    IGenericRepository<T> Repository<T>() where T : class;
    public void BeginTransaction();
    public void CommitTransaction();
    public void RollbackTransaction();
    public int Complete();
}