using EventApp.DataAccess.Data;
using EventApp.DataAccess.Repository.IRepository;
using EventApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EventApp.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class CategoryController : Controller
{
    private readonly IUnitOfWork _unitOfWork;

    public CategoryController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var categories = _unitOfWork.Category.GetAll().ToList();
        return Ok(categories);
    }
        
    [HttpGet("{id}")]
    public IActionResult Get(int id)
    {
        var category = _unitOfWork.Category.Get(c => c.Id == id);
        if (category == null)
        {
            return NotFound();
        }
        return Ok(category);
    }
        
    [HttpPost]
    public IActionResult Create([FromBody] Category category)
    {
        if (category == null)
        {
            return BadRequest();
        }
        _unitOfWork.Category.Add(category);
        _unitOfWork.Save();
        return CreatedAtAction(nameof(Get), new { id = category.Id }, category);
    }
        
    [HttpPut("{id}")]
    public IActionResult Update(int id, [FromBody] Category category)
    {
        if (category == null || category.Id != id)
        {
            return BadRequest();
        }

        _unitOfWork.Category.Update(category);
        _unitOfWork.Save();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var category = _unitOfWork.Category.Get(c => c.Id == id);
        if (category == null)
        {
            return NotFound();
        }

        _unitOfWork.Category.Remove(category);
        _unitOfWork.Save();
        return NoContent();
    }
}