using EightApp.Demo.EfCoreCodeFirst01.Interfaces;
using EightApp.Demo.EfCoreCodeFirst01.Models;
using Microsoft.EntityFrameworkCore;

namespace EightApp.Demo.EfCoreCodeFirst01.Repositories
{
    /// <summary>
    /// Repository for accessing and modifying Publisher entities.
    /// </summary>
    public class PublisherRepository : BaseRepository<Publisher>, IPublisherRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PublisherRepository"/> class.
        /// </summary>
        /// <param name="context">The DbContext to use for database access.</param>
        public PublisherRepository(LibraryContext context) : base(context)
        {
        }

        /// <inheritdoc/>
        public override async Task AddAsync(Publisher publisher)
        {
            // TODO: Add any additional validation logic before adding to the database.

            await base.AddAsync(publisher);
        }

        /// <inheritdoc/>
        public override void Update(Publisher publisher)
        {
            // TODO: Add any additional validation logic before updating in the database.

            base.Update(publisher);
        }

        /// <inheritdoc/>
        public override void Delete(Publisher publisher)
        {
            // TODO: Add any additional validation logic before deleting from the database.

            base.Delete(publisher);
        }

        /// <inheritdoc/>
        public override async Task<IEnumerable<Publisher>> GetAllAsync()
        {
            return await _context.Publishers.ToListAsync();
        }

        /// <inheritdoc/>
        public override async Task<Publisher?> GetByIdAsync(int id)
        {
            return await _context.Publishers.FindAsync(id);
        }
    }
}