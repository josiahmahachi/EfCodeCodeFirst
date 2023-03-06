using EightApp.Demo.EfCoreCodeFirst01.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;

namespace EightApp.Demo.EfCoreCodeFirst01.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly LibraryContext _context;

        public AuthorController(LibraryContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Retrieves all authors.
        /// </summary>
        [HttpGet]
        [SwaggerOperation("GetAllAuthors")]
        [ProducesResponseType(typeof(IEnumerable<Author>), 200)]
        public async Task<ActionResult<IEnumerable<Author>>> GetAuthors()
        {
            return await _context.Authors.ToListAsync();
        }

        /// <summary>
        /// Retrieves a specific author by ID.
        /// </summary>
        /// <param name="id">The ID of the author to retrieve.</param>
        [HttpGet("{id}")]
        [SwaggerOperation("GetAuthorById")]
        [ProducesResponseType(typeof(Author), 200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<Author>> GetAuthor(int id)
        {
            var author = await _context.Authors.FindAsync(id);

            if (author == null)
            {
                return NotFound();
            }

            return author;
        }

        /// <summary>
        /// Adds a new author to the database.
        /// </summary>
        /// <param name="author">The author to add.</param>
        [HttpPost]
        [SwaggerOperation("AddAuthor")]
        [ProducesResponseType(typeof(Author), 201)]
        public async Task<ActionResult<Author>> PostAuthor(Author author)
        {
            _context.Authors.Add(author);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAuthor", new { id = author.Id }, author);
        }

        /// <summary>
        /// Updates an existing author in the database.
        /// </summary>
        /// <param name="id">The ID of the author to update.</param>
        /// <param name="author">The updated author information.</param>
        [HttpPut("{id}")]
        [SwaggerOperation("UpdateAuthor")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> PutAuthor(int id, Author author)
        {
            if (id != author.Id)
            {
                return BadRequest();
            }

            _context.Entry(author).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AuthorExists(id))
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
        /// Deletes a author from the database.
        /// </summary>
        /// <param name="id">The ID of the author to delete.</param>
        [HttpDelete("{id}")]
        [SwaggerOperation("DeleteAuthor")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteAuthor(int id)
        {
            var author = await _context.Authors.FindAsync(id);
            if (author == null)
            {
                return NotFound();
            }

            _context.Authors.Remove(author);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AuthorExists(int id)
        {
            return _context.Authors.Any(e => e.Id == id);
        }
    }
}
