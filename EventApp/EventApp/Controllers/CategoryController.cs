using EventApp.DataAccess.Repository.IRepository;
using EventApp.Models;
using EventApp.Models.Dtos;
using EventApp.Utility;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EventApp.Controllers;

[ApiController]
[Route("/api/[controller]")]
[Authorize(Roles = SD.AdminRole)]
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
        var categoryDto = category.Adapt<CategoryDto>();
        return Ok(categoryDto);
    }
        
    [HttpPost]
    public async Task<IActionResult> CreateNewCategory([FromBody] CategoryDto? categoryDto)
    {
        if (categoryDto == null)
        {
            return BadRequest();
        }
        if (string.IsNullOrEmpty(categoryDto.CategoryName))
        {
            return BadRequest("Category name cannot be null or empty.");
        }
        var category = new Category
        {
            CategoryName = categoryDto.CategoryName
        };
        var result = await unitOfWork.Categories.AddAsync(category);
        await unitOfWork.CompleteAsync();
        return Ok(result.Adapt<CategoryDto>()
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
        if (string.IsNullOrEmpty(categoryDto.CategoryName))
        {
            return BadRequest("Category name cannot be null or empty.");
        }
        category.CategoryName = categoryDto.CategoryName;
        
        unitOfWork.Categories.Update(category);
        await unitOfWork.CompleteAsync();
        
        return Ok(category.Adapt<CategoryDto>());
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