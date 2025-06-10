using EventApp.DataAccess.Repository.IRepository;
using EventApp.Models;
using EventApp.Models.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EventApp.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class CategoryController(IUnitOfWork unitOfWork) : Controller
{
    [HttpGet]
    public async Task<IActionResult> GetAllCategories()
    {
        var categories = await unitOfWork.Categories.GetAllAsync();
        return Ok(categories);
    }
        
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetCategoryById(int id)
    {
        var category = await unitOfWork.Categories.GetByIdAsync(id);
        if (category == null)
        {
            return NotFound();
        }
        return Ok(category);
    }
        
    [HttpPost]
    public async Task<IActionResult> CreateNewCategory([FromBody] CategoryDto? categoryDto)
    {
        if (categoryDto == null)
        {
            return BadRequest();
        }
        var category = new Category
        {
            CategoryName = categoryDto.CategoryName
        };
        await unitOfWork.Categories.AddAsync(category);
        await unitOfWork.CompleteAsync();
        return CreatedAtAction(
            nameof(GetCategoryById), 
            new { id = category.Id }, 
            category
        );

    }
        
    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateCategoryName(int id, [FromBody] CategoryDto? categoryDto)
    {
        if (categoryDto == null)
        {
            return BadRequest();
        }
        
        var category = await unitOfWork.Categories.GetByIdAsync(id);
        if (category == null)
        {
            return NotFound();
        }
        category.CategoryName = categoryDto.CategoryName;
        
        unitOfWork.Categories.Update(category);
        await unitOfWork.CompleteAsync();
        
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteCategory(int id)
    {
        var category = await unitOfWork.Categories.GetByIdAsync(id);
        if (category == null)
        {
            return NotFound();
        }

        unitOfWork.Categories.Remove(category);
        await unitOfWork.CompleteAsync();
        return NoContent();
    }
}