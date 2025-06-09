// using EventApp.DataAccess.Data;
// using EventApp.DataAccess.Repository.IRepository;
// using EventApp.Models;
// using Microsoft.AspNetCore.Authorization;
// using Microsoft.AspNetCore.Http;
// using Microsoft.AspNetCore.Mvc;
//
// namespace EventApp.Controllers;
//
// [ApiController]
// [Route("/api/[controller]")]
// [Authorize]
// public class CategoryController(IUnitOfWork unitOfWork) : Controller
// {
//     [HttpGet]
//     public async Task<IActionResult> GetAll()
//     {
//         var categories = await unitOfWork.Categories.GetAllAsync();
//         return Ok(categories);
//     }
//         
//     [HttpGet("{id}")]
//     public async Task<IActionResult> Get(int id)
//     {
//         var category = await unitOfWork.Categories.GetByIdAsync(id);
//         if (category == null)
//         {
//             return NotFound();
//         }
//         return Ok(category);
//     }
//         
//     [HttpPost]
//     public async IActionResult Create([FromBody] Category category)
//     {
//         if (category == null)
//         {
//             return BadRequest();
//         }
//         await unitOfWork.Categories.AddAsync(category);
//         unitOfWork.Save();
//         return CreatedAtAction(nameof(Get), new { id = category.Id }, category);
//     }
//         
//     [HttpPut("{id}")]
//     public IActionResult Update(int id, [FromBody] Category category)
//     {
//         if (category == null || category.Id != id)
//         {
//             return BadRequest();
//         }
//
//         unitOfWork.Categories.Update(category);
//         unitOfWork.CompleteAsync();
//         return NoContent();
//     }
//
//     [HttpDelete("{id}")]
//     public IActionResult Delete(int id)
//     {
//         var category = unitOfWork.Categories.GetByIdAsync(id);
//         if (category == null)
//         {
//             return NotFound();
//         }
//
//         unitOfWork.Categories.Remove(category.Result);
//         unitOfWork.CompleteAsync();
//         return NoContent();
//     }
// }