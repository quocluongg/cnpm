using EventApp.DataAccess.Repository.IRepository;
using EventApp.Models;
using EventApp.Models.Dtos;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EventApp.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class OrderDetailController(IUnitOfWork unitOfWork) : Controller
{
    [HttpGet]
    public async Task<IActionResult> GetOrderDetails()
    {
        var orderDetails = await unitOfWork.OrderDetails.GetAllAsync();
        var orderDetailDtos = orderDetails.Select(orderDetail => orderDetail.Adapt<OrderDetailDto>());
        return Ok(orderDetailDtos);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetOrderDetailById(int id)
    {
        var orderDetail = await unitOfWork.OrderDetails.GetByIdAsync(id);
        if (orderDetail == null) return NotFound();
        var dto = orderDetail.Adapt<OrderDetailDto>();
        return Ok(dto);
    }
    
    [HttpGet("/order/{orderId:int}")]
    public async Task<IActionResult> GetOrderDetailsByOrderId(int orderId)
    {
        var orderDetails = await unitOfWork.OrderDetails.FindAsync(od => od.OrderId == orderId);
        var details = orderDetails as OrderDetail[] ?? orderDetails.ToArray();
        if (!details.Any()) return NotFound("No order details found for this order.");
        var orderDetailDtos = details.Select(orderDetail => orderDetail.Adapt<OrderDetailDto>());
        return Ok(orderDetailDtos);
    }

    [HttpPost]
    public async Task<IActionResult> CreateOrderDetail(OrderDetailDto dto)
    {
        // if (!ModelState.IsValid) return BadRequest(ModelState);
        var orderDetail = dto.Adapt<OrderDetail>();
        var result = await unitOfWork.OrderDetails.AddAsync(orderDetail);
        var order = await unitOfWork.Orders.GetByIdAsync(result.OrderId);
        if (order == null) return NotFound("Order not found");
        order.TotalAmount += result.Quantity * result.UnitPrice;
        unitOfWork.Orders.Update(order);
        await unitOfWork.CompleteAsync();
        return Ok(result.Adapt<OrderDetailDto>());
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateOrderDetail(int id, OrderDetailDto dto)
    {
        // if (!ModelState.IsValid) return BadRequest(ModelState);
        var orderDetail = await unitOfWork.OrderDetails.GetByIdAsync(id);
        if (orderDetail == null) return NotFound();
        
        var order = await unitOfWork.Orders.GetByIdAsync(orderDetail.OrderId);
        if (order == null) return NotFound("Order not found");
        // Update total amount
        order.TotalAmount -= orderDetail.Quantity * orderDetail.UnitPrice; // Subtract old values
        
        orderDetail = dto.Adapt<OrderDetail>();
        unitOfWork.OrderDetails.Update(orderDetail);
        order.TotalAmount += orderDetail.Quantity * orderDetail.UnitPrice; // Add new values
        unitOfWork.Orders.Update(order);
        
        await unitOfWork.CompleteAsync();
        
        return Ok(orderDetail.Adapt<OrderDto>());
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteOrderDetail(int id)
    {
        var orderDetail = await unitOfWork.OrderDetails.GetByIdAsync(id);
        if (orderDetail == null) return NotFound();
        var order = await unitOfWork.Orders.GetByIdAsync(orderDetail.OrderId);
        if (order == null) return NotFound("Order not found");
        order.TotalAmount -= orderDetail.Quantity * orderDetail.UnitPrice;
        unitOfWork.Orders.Update(order);
        
        unitOfWork.OrderDetails.Remove(orderDetail);
        await unitOfWork.CompleteAsync();
        
        return NoContent();
    }
}