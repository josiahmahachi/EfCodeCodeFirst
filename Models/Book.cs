using System.ComponentModel.DataAnnotations;

namespace EightApp.Demo.EfCoreCodeFirst01.Models
{
    public class Book : ModelBase
    {
        [Required]
        [StringLength(100)]
        public string Title { get; set; } = string.Empty;
        public int PublisherId { get; set; }
        public Publisher Publisher { get; set; } = new();
        public ICollection<Author> Authors { get; set; } = new List<Author>();
    }
}
