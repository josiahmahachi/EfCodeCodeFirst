using EightApp.Demo.EfCoreCodeFirst01.Interfaces;
using EightApp.Demo.EfCoreCodeFirst01.Models;

namespace EightApp.Demo.EfCoreCodeFirst01.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly LibraryContext _context;
        private Dictionary<Type, object> _repositories;

        public UnitOfWork(LibraryContext context)
        {
            _context = context;
            _repositories = new Dictionary<Type, object>();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public IRepositoryBase<T> GetRepository<T>() where T : ModelBase
        {
            if (_repositories.Keys.Contains(typeof(T)))
            {
                return _repositories[typeof(T)] as IRepositoryBase<T>;
            }

            var repository = new RepositoryBase<T>(_context);
            _repositories.Add(typeof(T), repository);

            return repository;
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }

}
