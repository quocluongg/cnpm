using Microsoft.AspNetCore.Mvc;
using EventApp.Models;
using EventApp.Models.Dtos;
using EventApp.DataAccess.Repository.IRepository;
using EventApp.Utility;
using Mapster;
using Microsoft.AspNetCore.Authorization;

namespace EventApp.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class OrderController(IUnitOfWork unitOfWork) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetOrders()
    {
        var orders = await unitOfWork.Orders.GetAllAsync();
        var orderDtos = orders.Select(eventEntity => eventEntity.Adapt<OrderDto>());
        return Ok(orderDtos);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetOrder(int id)
    {
        var order = await unitOfWork.Orders.GetByIdAsync(id);
        if (order == null) return NotFound();
        var dto = order.Adapt<OrderDto>();
        return Ok(dto);
    }
    
    [HttpGet("/user/{userId:int}")]
    public async Task<IActionResult> GetOrdersByUserId(int userId)
    {
        var orderDetails = await unitOfWork.Orders.FindAsync(o => o.UserId == userId);
        var details = orderDetails as Order[] ?? orderDetails.ToArray();
        if (!details.Any()) return NotFound("No orders found for this user.");
        var orderDtos = details.Select(order => order.Adapt<OrderDto>());
        return Ok(orderDtos);
    }

    [HttpPost]
    public async Task<IActionResult> CreateOrder(OrderDto dto)
    {
        // if (!ModelState.IsValid) return BadRequest(ModelState);
        var order = dto.Adapt<Order>();
        order.TotalAmount = 0;
        var result = await unitOfWork.Orders.AddAsync(order);
        await unitOfWork.CompleteAsync();
        return Ok(result.Adapt<OrderDto>());
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateOrder(int id, OrderDto dto)
    {
        // if (!ModelState.IsValid) return BadRequest(ModelState);
        var order = await unitOfWork.Orders.GetByIdAsync(id);
        if (order == null) return NotFound();
        
        order = dto.Adapt(order);
        unitOfWork.Orders.Update(order);
        await unitOfWork.CompleteAsync();
        
        return Ok(order.Adapt<OrderDto>());
    }
    
    [HttpPut("{id:int}/complete")]
    public async Task<IActionResult> UpdateOrderStatusToComplete(int id)
    {
        var order = await unitOfWork.Orders.GetByIdAsync(id);
        if (order == null) return NotFound();
        
        order.Status = SD.OrderStatusCompleted;
        unitOfWork.Orders.Update(order);
        await unitOfWork.CompleteAsync();
        
        return Ok(order.Adapt<OrderDto>());
    }
    
    [HttpPut("{id:int}/pending")]
    public async Task<IActionResult> UpdateOrderStatusToPending(int id)
    {
        var order = await unitOfWork.Orders.GetByIdAsync(id);
        if (order == null) return NotFound();
        
        order.Status = SD.OrderStatusPending;
        unitOfWork.Orders.Update(order);
        await unitOfWork.CompleteAsync();
        
        return Ok(order.Adapt<OrderDto>());
    }
    
    [HttpPut("{id:int}/cancel")]
    public async Task<IActionResult> UpdateOrderStatusToCancel(int id)
    {
        var order = await unitOfWork.Orders.GetByIdAsync(id);
        if (order == null) return NotFound();
        
        order.Status = SD.OrderStatusCancelled;
        unitOfWork.Orders.Update(order);
        await unitOfWork.CompleteAsync();
        
        return Ok(order.Adapt<OrderDto>());
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteOrder(int id)
    {
        var order = await unitOfWork.Orders.GetByIdAsync(id);
        if (order == null) return NotFound();
        
        unitOfWork.Orders.Remove(order);
        await unitOfWork.CompleteAsync();
        
        return NoContent();
    }
}