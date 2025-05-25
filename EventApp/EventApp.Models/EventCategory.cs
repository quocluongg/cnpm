using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace EventApp.Models;

public class EventCategory
{
    [Required]
    public int? EventId { get; set; }
    [ForeignKey("EventId")]
    [ValidateNever]
    public Event? Event { get; set; }

    [Required]
    public int CategoryId { get; set; }
    [ForeignKey("CategoryId")]
    [ValidateNever]
    public Category? Category { get; set; }

}