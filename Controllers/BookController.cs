using EightApp.Demo.EfCoreCodeFirst01.Interfaces;
using EightApp.Demo.EfCoreCodeFirst01.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;

namespace EightApp.Demo.EfCoreCodeFirst01.Controllers
{
    /// <summary>
    /// Controller for accessing book endpoints
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBookRepository _bookRepository;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="unitOfWork"></param>
        public BookController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _bookRepository = unitOfWork.GetRepository<IBookRepository, Book>();
        }

        /// <summary>
        /// Retrieves all books.
        /// </summary>
        [HttpGet]
        [SwaggerOperation("GetAllBooks")]
        [ProducesResponseType(typeof(IEnumerable<Book>), 200)]
        public async Task<ActionResult<IEnumerable<Book>>> GetBooks()
        {
            var books = await _bookRepository.GetAllAsync();

            return Ok(books);
        }

        /// <summary>
        /// Retrieves a specific book by ID.
        /// </summary>
        /// <param name="id">The ID of the book to retrieve.</param>
        [HttpGet("{id}")]
        [SwaggerOperation("GetBookById")]
        [ProducesResponseType(typeof(Book), 200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<Book>> GetBook(int id)
        {
            var book = await _bookRepository.GetByIdAsync(id);

            if (book == null)
            {
                return NotFound();
            }

            return Ok(book);
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
            var books = await _bookRepository.GetBooksByAuthorId(id);

            if (books is null)
            {
                return NotFound();
            }

            return Ok(books);
        }

        /// <summary>
        /// Adds a new book to the database.
        /// </summary>
        /// <param name="book">The book to add.</param>
        [HttpPost]
        [SwaggerOperation("AddBook")]
        [ProducesResponseType(typeof(Book), 201)]
        public async Task<ActionResult<Book>> PostBook(Book book)
        {
            await _bookRepository.AddAsync(book);
            await _unitOfWork.SaveAsync();

            return CreatedAtAction("GetBook", new { id = book.Id }, book);
        }

        /// <summary>
        /// Updates an existing book in the database.
        /// </summary>
        /// <param name="id">The ID of the book to update.</param>
        /// <param name="book">The updated book information.</param>
        [HttpPut("{id}")]
        [SwaggerOperation("UpdateBook")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> PutBook(int id, Book book)
        {
            if (id != book.Id)
            {
                return BadRequest();
            }

            _bookRepository.Update(book);

            try
            {
                await _unitOfWork.SaveAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await BookExists(id))
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
        /// Deletes a book from the database.
        /// </summary>
        /// <param name="id">The ID of the book to delete.</param>
        [HttpDelete("{id}")]
        [SwaggerOperation("DeleteBook")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var book = await _bookRepository.GetByIdAsync(id);

            if (book == null)
            {
                return NotFound();
            }

            _bookRepository.Delete(book);

            await _unitOfWork.SaveAsync();

            return NoContent();
        }

        private async Task<bool> BookExists(int id)
        {
            return await _bookRepository.ExistsAsync(id);
        }
    }
}
