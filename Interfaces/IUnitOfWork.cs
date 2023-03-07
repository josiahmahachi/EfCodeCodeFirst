using EightApp.Demo.EfCoreCodeFirst01.Models;

namespace EightApp.Demo.EfCoreCodeFirst01.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        TRepository GetRepository<TRepository, TEntity>() where TRepository : class, IRepositoryBase<TEntity> where TEntity : ModelBase;
        Task SaveAsync();
    }
}
