using System.ComponentModel.DataAnnotations;

namespace EightApp.Demo.EfCoreCodeFirst01.Models
{
    /// <summary>
    /// The book model
    /// </summary>
    public class Book : ModelBase
    {
        /// <summary>
        /// Title of the book
        /// </summary>
        [Required]
        [StringLength(100)]
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// Foreign key to the publisher
        /// </summary>
        public int PublisherId { get; set; }

        /// <summary>
        /// Relationship to the publisher model
        /// </summary>
        public Publisher Publisher { get; set; } = new();

        /// <summary>
        /// List of authors for the book
        /// </summary>
        public ICollection<Author> Authors { get; set; } = new List<Author>();
    }
}