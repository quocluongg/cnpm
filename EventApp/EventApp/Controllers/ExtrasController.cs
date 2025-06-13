using Microsoft.AspNetCore.Mvc;
using EventApp.Models;
using EventApp.Models.Dtos;
using EventApp.DataAccess.Repository.IRepository;
using Mapster;
using Microsoft.AspNetCore.Authorization;

namespace EventApp.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ExtrasController(IUnitOfWork unitOfWork) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ExtrasDto>>> GetAll()
    {
        var extras = await unitOfWork.Extras.GetAllAsync();
        var extrasEnumerable = extras.ToList();
        if (!extrasEnumerable.Any())
        {
            return NotFound();
        }
        var extrasDtos = extrasEnumerable.Select(extra => extra.Adapt<ExtrasDto>()).ToList();
        return Ok(extrasDtos);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ExtrasDto>> Get(int id)
    {
        var extra = await unitOfWork.Extras.GetByIdAsync(id);
        if (extra == null) return NotFound();
        var dto = extra.Adapt<ExtrasDto>();
        return Ok(dto);
    }

    [HttpPost]
    public async Task<ActionResult<ExtrasDto>> Create(ExtrasDto? dto)
    {
        if (dto == null) return BadRequest();

        if (string.IsNullOrEmpty(dto.ItemName))
        {
            return BadRequest("Item name cannot be null or empty.");
        }
        if (dto.Price <= 0)
        {
            return BadRequest("Price must be greater than 0.");
        }
        if (string.IsNullOrEmpty(dto.Description))
        {
            return BadRequest("Description cannot be null or empty.");
        }
        
        var extra = new Extras
        {
            ItemName = dto.ItemName,
            Price = dto.Price,
            Description = dto.Description
        };
        var result = await unitOfWork.Extras.AddAsync(extra);
        await unitOfWork.CompleteAsync();
        return Ok(result.Adapt<ExtrasDto>());
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, ExtrasDto? dto)
    {
        if (dto == null) return BadRequest();
        var extra = await unitOfWork.Extras.GetByIdAsync(id);
        if (extra == null) return NotFound();
        
        if (!string.IsNullOrEmpty(dto.ItemName))
        {
            extra.ItemName = dto.ItemName;

        }
        
        if (dto.Price > 0)
        {
            extra.Price = dto.Price;
        }
        if (!string.IsNullOrEmpty(dto.Description))
        {
            extra.Description = dto.Description;
        }

        unitOfWork.Extras.Update(extra);
        await unitOfWork.CompleteAsync();
        return Ok(extra.Adapt<ExtrasDto>());
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var extra = await unitOfWork.Extras.GetByIdAsync(id);
        if (extra == null) return NotFound();
        
        unitOfWork.Extras.Remove(extra);
        await unitOfWork.CompleteAsync();
        return NoContent();
    }
}