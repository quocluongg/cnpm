using EventApp.DataAccess.Repository.IRepository;
using EventApp.Models;
using EventApp.Models.Dtos;
using EventApp.Utility;
using Mapster;
using Microsoft.AspNetCore.Mvc;

namespace EventApp.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class TicketController(IUnitOfWork unitOfWork) : Controller
{
    [HttpGet]
    public async Task<IActionResult> GetAllTickets()
    {
        var tickets = await unitOfWork.Tickets.GetAllAsync();
        var ticketDtos = tickets.Select(ticket => ticket.Adapt<TicketDto>()).ToList();
        return Ok(tickets);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetTicketById(int id)
    {
        var ticket = await unitOfWork.Tickets.GetByIdAsync(id);
        if (ticket == null)
        {
            return NotFound();
        }
        var ticketDto = ticket.Adapt<TicketDto>();
        return Ok(ticketDto);
    }

    [HttpPost]
    public async Task<IActionResult> CreateNewTicket([FromBody] TicketDto? ticketDto)
    {
        if (ticketDto == null)
        {
            return BadRequest();
        }
        var ticket = new Ticket
        {
            OrderDetailId = ticketDto.OrderDetailId,
            TicketCode = ticketDto.TicketCode,
            Status = SD.TicketStatusValid,
        };
        var result = await unitOfWork.Tickets.AddAsync(ticket);
        await unitOfWork.CompleteAsync();
        return Ok(result.Adapt<TicketDto>());
    }
    
    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateTicket(int id, [FromBody] TicketDto? ticketDto)
    {
        if (ticketDto == null)
        {
            return BadRequest();
        }

        var ticket = await unitOfWork.Tickets.GetByIdAsync(id);
        if (ticket == null)
        {
            return NotFound();
        }

        ticket.OrderDetailId = ticketDto.OrderDetailId;
        ticket.TicketCode = ticketDto.TicketCode;
        if (string.IsNullOrEmpty(ticketDto.Status))
        {
            ticket.Status = ticketDto.Status;
        }

        unitOfWork.Tickets.Update(ticket);
        await unitOfWork.CompleteAsync();
        
        return Ok(ticket.Adapt<TicketDto>());
    }
    
    [HttpPut("{id:int}/invalidate")]
    public async Task<IActionResult> InvalidateTicket(int id)
    {
        var ticket = await unitOfWork.Tickets.GetByIdAsync(id);
        if (ticket == null)
        {
            return NotFound();
        }

        ticket.Status = SD.TicketStatusInvalid;
        unitOfWork.Tickets.Update(ticket);
        await unitOfWork.CompleteAsync();

        return Ok(ticket.Adapt<TicketDto>());
    }
    
    [HttpPut("{id:int}/validate")]
    public async Task<IActionResult> ValidateTicket(int id)
    {
        var ticket = await unitOfWork.Tickets.GetByIdAsync(id);
        if (ticket == null)
        {
            return NotFound();
        }

        ticket.Status = SD.TicketStatusValid;
        unitOfWork.Tickets.Update(ticket);
        await unitOfWork.CompleteAsync();

        return Ok(ticket.Adapt<TicketDto>());
    }
    
    [HttpPut("{id:int}/expire")]
    public async Task<IActionResult> ExpireTicket(int id)
    {
        var ticket = await unitOfWork.Tickets.GetByIdAsync(id);
        if (ticket == null)
        {
            return NotFound();
        }

        ticket.Status = SD.TicketStatusExpired;
        unitOfWork.Tickets.Update(ticket);
        await unitOfWork.CompleteAsync();

        return Ok(ticket.Adapt<TicketDto>());
    }
    
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteTicket(int id)
    {
        var ticket = await unitOfWork.Tickets.GetByIdAsync(id);
        if (ticket == null)
        {
            return NotFound();
        }

        unitOfWork.Tickets.Remove(ticket);
        await unitOfWork.CompleteAsync();

        return NoContent();
    }
}