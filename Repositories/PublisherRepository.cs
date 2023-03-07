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
    }
}