using EightApp.Demo.EfCoreCodeFirst01.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;

namespace EightApp.Demo.EfCoreCodeFirst01.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublisherController : ControllerBase
    {
        private readonly LibraryContext _context;

        public PublisherController(LibraryContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Retrieves all publishers.
        /// </summary>
        [HttpGet]
        [SwaggerOperation("GetAllPublishers")]
        [ProducesResponseType(typeof(IEnumerable<Publisher>), 200)]
        public async Task<ActionResult<IEnumerable<Publisher>>> GetPublishers()
        {
            return await _context.Publishers.ToListAsync();
        }

        /// <summary>
        /// Retrieves a specific publisher by ID.
        /// </summary>
        /// <param name="id">The ID of the publisher to retrieve.</param>
        [HttpGet("{id}")]
        [SwaggerOperation("GetPublisherById")]
        [ProducesResponseType(typeof(Publisher), 200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<Publisher>> GetPublisher(int id)
        {
            var publisher = await _context.Publishers.FindAsync(id);

            if (publisher == null)
            {
                return NotFound();
            }

            return publisher;
        }

        /// <summary>
        /// Updates an existing publisher in the database.
        /// </summary>
        /// <param name="id">The ID of the publisher to update.</param>
        /// <param name="publisher">The updated publisher information.</param>
        [HttpPut("{id}")]
        [SwaggerOperation("UpdatePublisher")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> PutPublisher(int id, Publisher publisher)
        {
            if (id != publisher.Id)
            {
                return BadRequest();
            }

            _context.Entry(publisher).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PublisherExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        /// <summary>
        /// Adds a new publisher to the database.
        /// </summary>
        /// <param name="publisher">The publisher to add.</param>
        [HttpPost]
        [SwaggerOperation("AddPublisher")]
        [ProducesResponseType(typeof(Publisher), 201)]
        public async Task<ActionResult<Publisher>> PostPublisher(Publisher publisher)
        {
            _context.Publishers.Add(publisher);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPublisher", new { id = publisher.Id }, publisher);
        }

        /// <summary>
        /// Deletes a publisher from the database.
        /// </summary>
        /// <param name="id">The ID of the publisher to delete.</param>
        [HttpDelete("{id}")]
        [SwaggerOperation("DeletePublisher")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<Publisher>> DeletePublisher(int id)
        {
            var publisher = await _context.Publishers.FindAsync(id);
            if (publisher == null)
            {
                return NotFound();
            }

            _context.Publishers.Remove(publisher);
            await _context.SaveChangesAsync();

            return publisher;
        }

        private bool PublisherExists(int id)
        {
            return _context.Publishers.Any(e => e.Id == id);
        }
    }
}
