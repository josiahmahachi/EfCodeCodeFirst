using EightApp.Demo.EfCoreCodeFirst01.Models;

namespace EightApp.Demo.EfCoreCodeFirst01.Interfaces
{
    /// <summary>
    /// Interface for authors
    /// </summary>
    public interface IAuthorRepository : IRepositoryBase<Author>
    {
        /// <summary>
        /// Get the books writen by the author
        /// </summary>
        /// <param name="authorId"></param>
        /// <returns></returns>
        Task<IEnumerable<Book>> GetBooksByAuthorAsync(int authorId);
    }
}


