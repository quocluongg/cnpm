using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EventApp.Models;

public class Extras
{
    [Key]
    public int Id { get; set; }

    [Required, StringLength(100)]
    public string ItemName { get; set; }

    [Required, Range(0, 10000)]
    [Column(TypeName = "decimal(18,2)")]
    public decimal Price { get; set; }

    [StringLength(500)]
    public string Description { get; set; }

    // Navigation property
    public ICollection<ExtrasOrder> ExtrasOrders { get; set; } = new List<ExtrasOrder>();
}