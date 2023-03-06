using System.ComponentModel.DataAnnotations;

namespace EightApp.Demo.EfCoreCodeFirst01.Models
{
    /// <summary>
    /// The publisher model
    /// </summary>
    public class Publisher : ModelBase
    {
        /// <summary>
        /// Name of the publisher
        /// </summary>
        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// List of books by the publisher
        /// </summary>
        public ICollection<Book> Books { get; set; } = new List<Book>();
    }
}
