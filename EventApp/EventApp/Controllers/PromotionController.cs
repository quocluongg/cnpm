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
public class PromotionController(IUnitOfWork unitOfWork) : Controller
{
    [HttpPost]
    public async Task<IActionResult> CreatePromotion([FromBody] PromotionDto? promotionDto)
    {
        if (promotionDto == null)
        {
            return BadRequest("Promotion data is required.");
        }
        
        if (promotionDto.DiscountPercent is  < 0 or > 100)
        {
            return BadRequest("Discount percent must be between 0 and 100.");
        }
        
        if (promotionDto.StartDate <= DateTime.UtcNow)
        {
            return BadRequest("Start date must be in the future.");
        }
        
        if (promotionDto.EndDate <= promotionDto.StartDate)
        {
            return BadRequest("End date must be after the start date.");
        }
        
        if (promotionDto.UsageLimit <= 0)
        {
            return BadRequest("Usage limit must be a positive number.");
        }

        if (string.IsNullOrWhiteSpace(promotionDto.PromotionCode))
        {
            return BadRequest("Promotion code is required.");
        }

        promotionDto.OrderIds = null; // Initialize OrderIds to null or an empty list if needed

        var promotion = promotionDto.Adapt<Promotion>();
        
        var result = await unitOfWork.Promotions.AddAsync(promotion);
        await unitOfWork.CompleteAsync();
        
        return Created("", result.Adapt<PromotionDto>());
    }
    
    [HttpPut("{id:int}/apply/{orderId:int}")]
    public async Task<IActionResult> ApplyPromotion(int id, int orderId)
    {
        var promotion = await unitOfWork.Promotions.GetByIdAsync(id);
        if (promotion == null)
        {
            return NotFound($"Promotion with ID {id} not found.");
        }

        var order = await unitOfWork.Orders.GetByIdAsync(orderId);
        if (order == null)
        {
            return NotFound($"Order with ID {orderId} not found.");
        }
        
        var promotionOrder = new PromotionOrder
        {
            PromotionId = promotion.Id,
            OrderId = order.Id,
        };
        await unitOfWork.PromotionOrders.AddAsync(promotionOrder);
        
        promotion.UsageLimit--;
        unitOfWork.Promotions.Update(promotion);
        
        await unitOfWork.CompleteAsync();
        
        var dto = promotion.Adapt<PromotionDto>();
        var listOfAppliedOrders = await unitOfWork.PromotionOrders.FindAsync(po => po.PromotionId == promotion.Id);
        dto.OrderIds = listOfAppliedOrders.Select(po => po.OrderId).ToList();
        
        return Ok(dto);
    }
    
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetPromotionById(int id)
    {
        var promotion = await unitOfWork.Promotions.GetByIdAsync(id);
        if (promotion == null)
        {
            return NotFound($"Promotion with ID {id} not found.");
        }
        
        var dto = promotion.Adapt<PromotionDto>();
        var listOfAppliedOrders = await unitOfWork.PromotionOrders.FindAsync(po => po.PromotionId == promotion.Id);
        dto.OrderIds = listOfAppliedOrders.Select(po => po.OrderId).ToList();
        
        return Ok(dto);
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAllPromotions()
    {
        var promotions = await unitOfWork.Promotions.GetAllAsync();
        var enumerable = promotions as Promotion[] ?? promotions.ToArray();
        if (!enumerable.Any())
        {
            return NotFound("No promotions found.");
        }
        
        var promotionDtos = enumerable.Select(p => p.Adapt<PromotionDto>());
        
        return Ok(promotionDtos);
    }
    
    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdatePromotion(int id, [FromBody] PromotionDto? promotionDto)
    {
        if (promotionDto == null)
        {
            return BadRequest("Promotion data is required.");
        }
        
        var existingPromotion = await unitOfWork.Promotions.GetByIdAsync(id);
        if (existingPromotion == null)
        {
            return NotFound($"Promotion with ID {id} not found.");
        }

        if (promotionDto.DiscountPercent is < 0 or > 100)
        {
            return BadRequest("Discount percent must be between 0 and 100.");
        }
        
        if (promotionDto.StartDate <= DateTime.UtcNow)
        {
            return BadRequest("Start date must be in the future.");
        }
        
        if (promotionDto.EndDate <= promotionDto.StartDate)
        {
            return BadRequest("End date must be after the start date.");
        }
        
        if (promotionDto.UsageLimit > 0)
        {
            existingPromotion.UsageLimit = promotionDto.UsageLimit;
        }
        
        if (!string.IsNullOrWhiteSpace(promotionDto.PromotionCode))
        {
            existingPromotion.PromotionCode = promotionDto.PromotionCode;
        }
        
        existingPromotion.DiscountPercent = promotionDto.DiscountPercent;
        existingPromotion.StartDate = promotionDto.StartDate;
        existingPromotion.EndDate = promotionDto.EndDate;
        existingPromotion.Description = promotionDto.Description;
        
        unitOfWork.Promotions.Update(existingPromotion);
        await unitOfWork.CompleteAsync();
        
        return Ok(existingPromotion.Adapt<PromotionDto>());
    }
    
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeletePromotion(int id)
    {
        var promotion = await unitOfWork.Promotions.GetByIdAsync(id);
        if (promotion == null)
        {
            return NotFound($"Promotion with ID {id} not found.");
        }
        
        unitOfWork.Promotions.Remove(promotion);
        await unitOfWork.CompleteAsync();
        
        return NoContent(); // Return 204 No Content on successful deletion
    }
}