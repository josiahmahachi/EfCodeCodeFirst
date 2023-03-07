using EightApp.Demo.EfCoreCodeFirst01.Interfaces;
using EightApp.Demo.EfCoreCodeFirst01.Models;
using Microsoft.EntityFrameworkCore;

namespace EightApp.Demo.EfCoreCodeFirst01.Repositories
{
    /// <summary>
    /// Repository for accessing and modifying Book entities.
    /// </summary>
    public class BookRepository : BaseRepository<Book>, IBookRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BookRepository"/> class.
        /// </summary>
        /// <param name="context">The DbContext to use for database access.</param>
        public BookRepository(LibraryContext context) : base(context)
        {
        }

        /// <inheritdoc/>
        public override async Task AddAsync(Book book)
        {
            // TODO: Add any additional validation logic before adding to the database.

            await base.AddAsync(book);
        }

        /// <inheritdoc/>
        public override void Update(Book book)
        {
            // TODO: Add any additional validation logic before updating in the database.

            base.Update(book);
        }

        /// <inheritdoc/>
        public override void Delete(Book book)
        {
            // TODO: Add any additional validation logic before deleting from the database.

            base.Delete(book);
        }

        /// <inheritdoc/>
        public override async Task<IEnumerable<Book>> GetAllAsync()
        {
            return await _context.Books.ToListAsync();
        }

        /// <inheritdoc/>
        public override async Task<Book?> GetByIdAsync(int id)
        {
            return await _context.Books.FindAsync(id);
        }
    }
}