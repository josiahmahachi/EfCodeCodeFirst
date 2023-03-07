using EightApp.Demo.EfCoreCodeFirst01.Interfaces;
using EightApp.Demo.EfCoreCodeFirst01.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;

namespace EightApp.Demo.EfCoreCodeFirst01.Controllers
{
    /// <summary>
    /// Controller for accessing author endpoints
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuthorRepository _authorRepository;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="unitOfWork"></param>
        public AuthorController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _authorRepository = unitOfWork.GetRepository<IAuthorRepository, Author>();
        }

        /// <summary>
        /// Retrieves all authors.
        /// </summary>
        [HttpGet]
        [SwaggerOperation("GetAllAuthors")]
        [ProducesResponseType(typeof(IEnumerable<Author>), 200)]
        public async Task<ActionResult<IEnumerable<Author>>> GetAuthors()
        {
            var authors = await _authorRepository.GetAllAsync();

            return Ok(authors);
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
            var author = await _authorRepository.GetByIdAsync(id);

            if (author == null)
            {
                return NotFound();
            }

            return Ok(author);
        }

        /// <summary>
        /// Retrieves books by a specific author.
        /// </summary>
        /// <param name="id">The ID of the author whose books to retrieve.</param>
        [HttpGet("GetBooksByAuthorById/{id}")]
        [SwaggerOperation("GetBooksByAuthorById")]
        [ProducesResponseType(typeof(Book), 200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<IEnumerable<Book>>> GetBooksByAuthor(int id)
        {
            var books = await _authorRepository.GetBooksByAuthorAsync(id);

            if (books is null)
            {
                return NotFound();
            }

            return Ok(books);
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
            await _authorRepository.AddAsync(author);
            await _unitOfWork.SaveAsync();

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

            _authorRepository.Update(author);

            try
            {
                await _unitOfWork.SaveAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await AuthorExists(id))
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
            var author = await _authorRepository.GetByIdAsync(id);

            if (author == null)
            {
                return NotFound();
            }

            _authorRepository.Delete(author);

            await _unitOfWork.SaveAsync();

            return NoContent();
        }

        private async Task<bool> AuthorExists(int id)
        {
            return await _authorRepository.ExistsAsync(id);
        }
    }
}
