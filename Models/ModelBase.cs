using System.ComponentModel.DataAnnotations;

namespace EightApp.Demo.EfCoreCodeFirst01.Models
{
    public abstract class ModelBase
    {
        [Key]
        public int Id { get; set; }
        public DateTime DateAdded { get; set; } = DateTime.UtcNow;
    }
}