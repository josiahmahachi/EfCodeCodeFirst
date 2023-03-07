using EightApp.Demo.EfCoreCodeFirst01.Interfaces;
using EightApp.Demo.EfCoreCodeFirst01.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;

namespace EightApp.Demo.EfCoreCodeFirst01.Controllers
{
    /// <summary>
    /// Controller for accessing publisher endpoints
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class PublisherController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPublisherRepository _publisherRepository;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="unitOfWork"></param>
        public PublisherController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _publisherRepository = unitOfWork.GetRepository<IPublisherRepository, Publisher>();
        }

        /// <summary>
        /// Retrieves all publishers.
        /// </summary>
        [HttpGet]
        [SwaggerOperation("GetAllPublishers")]
        [ProducesResponseType(typeof(IEnumerable<Publisher>), 200)]
        public async Task<ActionResult<IEnumerable<Publisher>>> GetPublishers()
        {
            var publishers = await _publisherRepository.GetAllAsync();

            return Ok(publishers);
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
            var publisher = await _publisherRepository.GetByIdAsync(id);

            if (publisher == null)
            {
                return NotFound();
            }

            return Ok(publisher);
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
            await _publisherRepository.AddAsync(publisher);
            await _unitOfWork.SaveAsync();

            return CreatedAtAction("GetPublisher", new { id = publisher.Id }, publisher);
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

            _publisherRepository.Update(publisher);

            try
            {
                await _unitOfWork.SaveAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await PublisherExists(id))
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
        /// Deletes a publisher from the database.
        /// </summary>
        /// <param name="id">The ID of the publisher to delete.</param>
        [HttpDelete("{id}")]
        [SwaggerOperation("DeletePublisher")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeletePublisher(int id)
        {
            var publisher = await _publisherRepository.GetByIdAsync(id);

            if (publisher == null)
            {
                return NotFound();
            }

            _publisherRepository.Delete(publisher);

            await _unitOfWork.SaveAsync();

            return NoContent();
        }

        private async Task<bool> PublisherExists(int id)
        {
            return await _publisherRepository.ExistsAsync(id);
        }
    }
}
