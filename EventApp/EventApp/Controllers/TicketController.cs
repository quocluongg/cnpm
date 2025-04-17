using EventApp.DataAccess.Repository.IRepository;
using EventApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EventApp.Controllers;

[ApiController]
[Route("/api/[controller]")]
[Authorize]
public class TicketController : Controller
{
	private readonly IUnitOfWork _unitOfWork;
  
      public TicketController(IUnitOfWork unitOfWork)
      {
          _unitOfWork = unitOfWork;
      }
  
      [HttpGet]
      public IActionResult GetAll()
      {
          var events = _unitOfWork.Ticket.GetAll().ToList();
          return Ok(events);
      }
          
      [HttpGet("{id}")]
      public IActionResult Get(int id)
      {
          var @event = _unitOfWork.Ticket.Get(c => c.Id == id);
          if (@event == null)
          {
              return NotFound();
          }
          return Ok(@event);
      }
          
      [HttpPost]
      public IActionResult Create([FromBody] Ticket ticket)
      {
          if (ticket == null)
          {
              return BadRequest();
          }
          _unitOfWork.Ticket.Add(ticket);
          _unitOfWork.Save();
          return CreatedAtAction(nameof(Get), new { id = ticket.Id }, ticket);
      }
          
      [HttpPut("{id}")]
      public IActionResult Update(int id, [FromBody] Ticket ticket)
      {
          if (ticket == null || ticket.Id != id)
          {
              return BadRequest();
          }
          
          _unitOfWork.Ticket.Update(ticket);
          _unitOfWork.Save();
          return NoContent();
      }
  
      [HttpDelete("{id}")]
      public IActionResult Delete(int id)
      {
          var ticket = _unitOfWork.Ticket.Get(c => c.Id == id);
          if (ticket == null)
          {
              return NotFound();
          }
  
          _unitOfWork.Ticket.Remove(ticket);
          _unitOfWork.Save();
          return NoContent();
      }
}