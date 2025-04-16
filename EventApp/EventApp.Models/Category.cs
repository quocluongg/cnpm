using System.ComponentModel.DataAnnotations;

namespace EventApp.Models
{
    public class Category
    {
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        [Range(1,100, ErrorMessage = "Sorting Order must be between 1 and 100.")]
        [Display(Name = "Sorting Order")]
        public int SortingOrder { get; set; }
    }
}
