using EightApp.Demo.EfCoreCodeFirst01.Interfaces;

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

        TRepository IUnitOfWork.GetRepository<TRepository, TEntity>()
        {
            var type = typeof(TEntity);

            if (_repositories.ContainsKey(type))
            {
                return (TRepository)_repositories[type];
            }
            else
            {
                var repositoryType = typeof(TRepository);
                var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(TEntity)), _context);
                _repositories.Add(type, repositoryInstance);
            }

            return (TRepository)_repositories[type];
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
