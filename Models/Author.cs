using System.ComponentModel.DataAnnotations;

namespace EightApp.Demo.EfCoreCodeFirst01.Models
{
    /// <summary>
    /// The author model
    /// </summary>
    public class Author : ModelBase
    {
        /// <summary>
        /// Name of the author
        /// </summary>
        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// List of book by the author
        /// </summary>
        public ICollection<Book> Books { get; set; } = new List<Book>();
    }
}
