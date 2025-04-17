using EventApp.DataAccess.Repository.IRepository;
using EventApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EventApp.Controllers;

[ApiController]
[Route("/api/[controller]")]
[Authorize]
public class EventController : Controller
{
	private readonly IUnitOfWork _unitOfWork;
  
      public EventController(IUnitOfWork unitOfWork)
      {
          _unitOfWork = unitOfWork;
      }
  
      [HttpGet]
      public IActionResult GetAll()
      {
          var events = _unitOfWork.Event.GetAll().ToList();
          return Ok(events);
      }
          
      [HttpGet("{id}")]
      public IActionResult Get(int id)
      {
          var @event = _unitOfWork.Event.Get(c => c.Id == id);
          if (@event == null)
          {
              return NotFound();
          }
          return Ok(@event);
      }
          
      [HttpPost]
      public IActionResult Create([FromBody] Event @event)
      {
          if (@event == null)
          {
              return BadRequest();
          }
          _unitOfWork.Event.Add(@event);
          _unitOfWork.Save();
          return CreatedAtAction(nameof(Get), new { id = @event.Id }, @event);
      }
          
      [HttpPut("{id}")]
      public IActionResult Update(int id, [FromBody] Event @event)
      {
          if (@event == null || @event.Id != id)
          {
              return BadRequest();
          }
          
          _unitOfWork.Event.Update(@event);
          _unitOfWork.Save();
          return NoContent();
      }
  
      [HttpDelete("{id}")]
      public IActionResult Delete(int id)
      {
          var @event = _unitOfWork.Event.Get(c => c.Id == id);
          if (@event == null)
          {
              return NotFound();
          }
  
          _unitOfWork.Event.Remove(@event);
          _unitOfWork.Save();
          return NoContent();
      }
}