using EightApp.Demo.EfCoreCodeFirst01.Models;

namespace EightApp.Demo.EfCoreCodeFirst01.Interfaces
{
    /// <summary>
    /// The book repository
    /// </summary>
    public interface IBookRepository : IRepositoryBase<Book>
    {
        /// <summary>
        /// Get books writen by a certain author
        /// </summary>
        /// <param name="authorId">Id of the authur</param>
        /// <returns></returns>
        Task<IEnumerable<Book>> GetBooksByAuthorId(int authorId);
    }
}