using EightApp.Demo.EfCoreCodeFirst01.Models;

namespace EightApp.Demo.EfCoreCodeFirst01.Interfaces
{
    public interface IRepositoryBase<T> where T : ModelBase
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T?> GetByIdAsync(int id);
        Task AddAsync(T entity);
        void Update(T entity);
        void Delete(T entity);
        Task<bool> ExistsAsync(int id);
    }
}
