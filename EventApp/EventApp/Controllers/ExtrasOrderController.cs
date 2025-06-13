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
public class ExtrasOrderController(IUnitOfWork unitOfWork) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ExtrasOrderDto>>> GetAll()
    {
        var extrasOrders = await unitOfWork.ExtrasOrders.GetAllAsync();
        var extrasOrderDtos = extrasOrders.Select(eo => eo.Adapt<ExtrasOrderDto>()).ToList();

        if (!extrasOrderDtos.Any())
        {
            return NotFound();
        }

        return Ok(extrasOrderDtos);
    }

    [HttpGet("{extrasId}/{orderId}")]
    public async Task<ActionResult<ExtrasOrderDto>> Get(int extrasId, int orderId)
    {
        var extrasOrder = (await unitOfWork.ExtrasOrders.FindAsync(eo => eo.ExtrasId == extrasId && eo.OrderId == orderId)).FirstOrDefault();
        if (extrasOrder == null) return NotFound();
        var dto = extrasOrder.Adapt<ExtrasOrderDto>();
        return Ok(dto);
    }

    [HttpPost]
    public async Task<ActionResult> Create(ExtrasOrderDto? dto)
    {
        if (dto == null) return BadRequest();
        var extrasOrder = dto.Adapt<ExtrasOrder>();
        var result = await unitOfWork.ExtrasOrders.AddAsync(extrasOrder);
        await unitOfWork.CompleteAsync();
        return Ok(result.Adapt<ExtrasOrderDto>());
    }

    [HttpPut]
    public async Task<IActionResult> Update(ExtrasOrderDto dto)
    {
        var extrasOrder = (await unitOfWork.ExtrasOrders.FindAsync(e => e.ExtrasId == dto.ExtrasId && e.OrderId == dto.OrderId)).FirstOrDefault();
        if (extrasOrder == null) return NotFound();
        
        extrasOrder.Quantity = dto.Quantity;
        unitOfWork.ExtrasOrders.Update(extrasOrder);
        await unitOfWork.CompleteAsync();
        
        return Ok(extrasOrder.Adapt<ExtrasOrderDto>());
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(int extrasId, int orderId)
    {
        var eo = (await unitOfWork.ExtrasOrders.FindAsync(e => e.ExtrasId == extrasId && e.OrderId == orderId)).FirstOrDefault();
        if (eo == null) return NotFound();
        unitOfWork.ExtrasOrders.Remove(eo);
        await unitOfWork.CompleteAsync();
        return NoContent();
    }
}