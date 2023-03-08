using EightApp.Demo.EfCoreCodeFirst01.Interfaces;
using EightApp.Demo.EfCoreCodeFirst01.Models;
using Microsoft.EntityFrameworkCore;

namespace EightApp.Demo.EfCoreCodeFirst01.Repositories
{
    /// <summary>
    /// Repository for accessing and modifying Author entities.
    /// </summary>
    public class AuthorRepository : RepositoryBase<Author>, IAuthorRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AuthorRepository"/> class.
        /// </summary>
        /// <param name="context">The DbContext to use for database access.</param>
        public AuthorRepository(LibraryContext context) : base(context)
        {
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<Book>> GetBooksByAuthorAsync(int authorId)
        {
            return await _context.Books.Where(b => b.Authors.Any(x => x.Id == authorId)).ToListAsync();
        }
    }
}