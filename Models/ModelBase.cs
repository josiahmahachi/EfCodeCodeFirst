using System.ComponentModel.DataAnnotations;

namespace EightApp.Demo.EfCoreCodeFirst01.Models
{
    /// <summary>
    /// The base model for all database entities
    /// </summary>
    public abstract class ModelBase
    {
        /// <summary>
        /// The ID of the model/table.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// The date when a record was added to the database
        /// </summary>
        [Required]
        public DateTime DateAdded { get; set; } = DateTime.UtcNow;
    }
}