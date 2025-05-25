using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace EventApp.Models;

public class EventCategory
{
    [ForeignKey("Event")]
    public int EventId { get; set; }

    [ForeignKey("Category")]
    public int CategoryId { get; set; }

    // Navigation properties
    public Event Event { get; set; }
    public Category Category { get; set; }
}