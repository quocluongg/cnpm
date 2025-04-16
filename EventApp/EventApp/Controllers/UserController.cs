using EventApp.DataAccess.Repository.IRepository;
using EventApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace EventApp.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class UserController : Controller
{
	private readonly IUnitOfWork _unitOfWork;
  
      public UserController(IUnitOfWork unitOfWork)
      {
          _unitOfWork = unitOfWork;
      }
  
      [HttpGet]
      public IActionResult GetAll()
      {
          var users = _unitOfWork.User.GetAll().ToList();
          return Ok(users);
      }
          
      [HttpGet("{id}")]
      public IActionResult Get(int id)
      {
          var user = _unitOfWork.User.Get(c => c.Id == id);
          if (user == null)
          {
              return NotFound();
          }
          return Ok(user);
      }
          
      [HttpPost]
      public IActionResult Create([FromBody] User user)
      {
          if (user == null)
          {
              return BadRequest();
          }
          _unitOfWork.User.Add(user);
          _unitOfWork.Save();
          return CreatedAtAction(nameof(Get), new { id = user.Id }, user);
      }
          
      [HttpPut("{id}")]
      public IActionResult Update(int id, [FromBody] User user)
      {
          if (user == null || user.Id != id)
          {
              return BadRequest();
          }
          
          _unitOfWork.User.Update(user);
          _unitOfWork.Save();
          return NoContent();
      }
  
      [HttpDelete("{id}")]
      public IActionResult Delete(int id)
      {
          var user = _unitOfWork.User.Get(c => c.Id == id);
          if (user == null)
          {
              return NotFound();
          }
  
          _unitOfWork.User.Remove(user);
          _unitOfWork.Save();
          return NoContent();
      }
}