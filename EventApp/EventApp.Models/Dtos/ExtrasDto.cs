namespace EventApp.Models.Dtos;

public class ExtrasDto
{
    public int Id { get; set; } = 0;
    public string? ItemName { get; set; }
    public decimal Price { get; set; } = 0;
    public string? Description { get; set; }
}