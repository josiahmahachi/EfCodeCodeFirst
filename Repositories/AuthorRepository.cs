using EightApp.Demo.EfCoreCodeFirst01.Interfaces;
using EightApp.Demo.EfCoreCodeFirst01.Models;
using Microsoft.EntityFrameworkCore;

namespace EightApp.Demo.EfCoreCodeFirst01.Repositories
{
    /// <summary>
    /// Repository for accessing and modifying Author entities.
    /// </summary>
    public class AuthorRepository : BaseRepository<Author>, IAuthorRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AuthorRepository"/> class.
        /// </summary>
        /// <param name="context">The DbContext to use for database access.</param>
        public AuthorRepository(LibraryContext context) : base(context)
        {
        }

        /// <inheritdoc/>
        public override async Task AddAsync(Author author)
        {
            // TODO: Add any additional validation logic before adding to the database.

            await base.AddAsync(author);
        }

        /// <inheritdoc/>
        public override void Update(Author author)
        {
            // TODO: Add any additional validation logic before updating in the database.

            base.Update(author);
        }

        /// <inheritdoc/>
        public override void Delete(Author author)
        {
            // TODO: Add any additional validation logic before deleting from the database.

            base.Delete(author);
        }

        /// <inheritdoc/>
        public override async Task<IEnumerable<Author>> GetAllAsync()
        {
            return await _context.Authors.ToListAsync();
        }

        /// <inheritdoc/>
        public override async Task<Author?> GetByIdAsync(int id)
        {
            return await _context.Authors.FindAsync(id);
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<Book>> GetBooksByAuthorAsync(int authorId)
        {
            return await _context.Books.Where(b => b.Authors.Any(x => x.Id == authorId)).ToListAsync();
        }
    }
}