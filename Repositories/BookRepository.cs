using EightApp.Demo.EfCoreCodeFirst01.Interfaces;
using EightApp.Demo.EfCoreCodeFirst01.Models;
using Microsoft.EntityFrameworkCore;

namespace EightApp.Demo.EfCoreCodeFirst01.Repositories
{
    /// <summary>
    /// Repository for accessing and modifying Book entities.
    /// </summary>
    public class BookRepository : RepositoryBase<Book>, IBookRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BookRepository"/> class.
        /// </summary>
        /// <param name="context">The DbContext to use for database access.</param>
        public BookRepository(LibraryContext context) : base(context)
        {
        }

        /// <summary>
        /// Get books writen by a certain author
        /// </summary>
        /// <param name="authorId">Id of the authur</param>
        /// <returns></returns>
        public async Task<IEnumerable<Book>> GetBooksByAuthorId(int authorId)
        {
            return await _context.Books.Where(b => b.Authors.Any(x => x.Id == authorId)).ToListAsync();
        }

        /// <summary>
        /// Add a book
        /// </summary>
        /// <param name="entity">The book entity</param>
        /// <returns>void</returns>
        public override async Task AddAsync(Book entity)
        {
            // Do something here before calling base.AddAsync(entity)

            await base.AddAsync(entity);
        }
    }
}