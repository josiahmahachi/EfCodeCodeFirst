using System.ComponentModel.DataAnnotations;

namespace EightApp.Demo.EfCoreCodeFirst01.Models
{
    public class Publisher : ModelBase
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;
        public ICollection<Book> Books { get; set; } = new List<Book>();
    }
}
