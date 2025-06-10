using EventApp.DataAccess.Repository.IRepository;
using EventApp.Models;
using EventApp.Models.Dtos;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EventApp.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class EventController(IUnitOfWork unitOfWork) : Controller
{
    [HttpGet]
    public async Task<IActionResult> GetAllEvents()
    {
        var events = await unitOfWork.Events.GetAllAsync();
        var eventEntities = events.ToList();
        if (!eventEntities.Any())
        {
            return NotFound();
        }
        
        var eventDtos = eventEntities.Select(eventEntity => eventEntity.Adapt<EventDto>()).ToList();
        
        return Ok(eventDtos);
    }
    
    [HttpGet("category/{categoryId:int}")]
    public async Task<IActionResult> GetEventsByCategory(int categoryId)
    {
        var events = await unitOfWork.Events.FindAsync(e => e.EventCategories.Any(ec => ec.CategoryId == categoryId));
        var eventEntities = events.ToList();
        if (!eventEntities.Any())
        {
            return NotFound();
        }
        
        var eventDtos = eventEntities.Select(eventEntity => eventEntity.Adapt<EventDto>()).ToList();

        return Ok(eventDtos);
    }
        
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetEventById(int id)
    {
        var eventItem = await unitOfWork.Events.GetByIdAsync(id);
        if (eventItem == null)
        {
            return NotFound();
        }
        
        var eventDto = eventItem.Adapt<EventDto>();
        
        return Ok(eventDto);
    }
        
    [HttpPost]
    public async Task<IActionResult> CreateNewEvent([FromBody] EventDto? eventDto)
    {
        if (eventDto == null)
        {
            return BadRequest();
        }
        
        // Map the EventDto to Event entity
        var existingEvent = await unitOfWork.Events.FindAsync(e => e.EventName == eventDto.EventName);
        if (existingEvent.Any())
        {
            return BadRequest("An event with this name already exists.");
        }
        
        var eventItem = eventDto.Adapt<Event>();
        
        // Set the CreatedDate to the current date and time
        eventItem.CreatedDate = DateTime.UtcNow;
        
        eventItem.UserId = eventDto.UserId;
        
        await unitOfWork.Events.AddAsync(eventItem);
        await unitOfWork.CompleteAsync();
        
        return CreatedAtAction(
            nameof(GetEventById), 
            new { id = eventItem.Id }, 
            eventItem
        );
    }
        
    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateInfoEvent(int id, [FromBody] EventDto? eventDto)
    {
        if (eventDto == null)
        {
            return BadRequest();
        }
        
        var eventItem = await unitOfWork.Events.GetByIdAsync(id);
        if (eventItem == null)
        {
            return NotFound();
        }
        
        // Map the properties from eventDto to eventItem
        eventItem.EventName = eventDto.EventName;
        eventItem.Description = eventDto.Description;
        eventItem.StartTime = eventDto.StartTime;
        eventItem.EndTime = eventDto.EndTime;
        eventItem.ImageUrl = eventDto.ImageUrl;
        eventItem.Location = eventDto.Location;
        eventItem.UserId = eventDto.UserId;

        unitOfWork.Events.Update(eventItem);
        await unitOfWork.CompleteAsync();
        return NoContent();
    }
    
    [HttpPut("{id:int}/image")]
    public async Task<IActionResult> UpdateEventImage(int id, [FromBody] string? imageUrl)
    {
        if (string.IsNullOrEmpty(imageUrl))
        {
            return BadRequest("Image URL cannot be null or empty.");
        }
        
        var eventItem = await unitOfWork.Events.GetByIdAsync(id);
        if (eventItem == null)
        {
            return NotFound();
        }
        
        eventItem.ImageUrl = imageUrl;
        unitOfWork.Events.Update(eventItem);
        await unitOfWork.CompleteAsync();
        return NoContent();
    }
    
    [HttpPost("{id:int}/ticket-types")]
    public async Task<IActionResult> AddTicketType(int id, [FromBody] TicketTypeDto? ticketTypeDto)
    {
        if (ticketTypeDto == null)
        {
            return BadRequest();
        }
        
        var eventItem = await unitOfWork.Events.GetByIdAsync(id);
        if (eventItem == null)
        {
            return NotFound();
        }
        
        var ticketType = ticketTypeDto.Adapt<TicketType>();
        ticketType.EventId = id; // Associate the ticket type with the event
        await unitOfWork.TicketTypes.AddAsync(ticketType);
        await unitOfWork.CompleteAsync();
        
        return CreatedAtAction(
            nameof(GetEventById), 
            new { id = eventItem.Id }, 
            ticketType
        );
    }
    
    [HttpPost("{id:int}/categories")]
    public async Task<IActionResult> AddCategoryToEvent(int id, [FromBody] int categoryId)
    {
        var eventItem = await unitOfWork.Events.GetByIdAsync(id);
        if (eventItem == null)
        {
            return NotFound();
        }
        
        var category = await unitOfWork.Categories.GetByIdAsync(categoryId);
        if (category == null)
        {
            return NotFound("Category not found.");
        }
        
        // Check if the event already has this category
        if (eventItem.EventCategories.Any(ec => ec.CategoryId == categoryId))
        {
            return BadRequest("This category is already associated with the event.");
        }
        
        eventItem.EventCategories.Add(new EventCategory { EventId = id, CategoryId = categoryId });
        unitOfWork.Events.Update(eventItem);
        await unitOfWork.CompleteAsync();
        
        return NoContent();
    }
    
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteEvent(int id)
    {
        var eventItem = await unitOfWork.Events.GetByIdAsync(id);
        if (eventItem == null)
        {
            return NotFound();
        }
        
        unitOfWork.Events.Remove(eventItem);
        await unitOfWork.CompleteAsync();
        return NoContent();
    }
}