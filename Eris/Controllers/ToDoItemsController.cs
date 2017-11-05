using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Eris.Models;

namespace Eris.Controllers
{
    [Produces("application/json")]
    [Route("api/TodoItems")]
    public class TodoItemsController : Controller
    {
        private readonly ErisDbContext _context;

        public TodoItemsController(ErisDbContext context)
        {
            _context = context;
        }

        // GET: api/TodoItems
        [HttpGet]
        public IEnumerable<TodoItem> GetToDoItem()
        {
            return _context.ToDoItem;
        }

        // GET: api/TodoItems/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetToDoItem([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var toDoItem = await _context.ToDoItem.SingleOrDefaultAsync(m => m.Id == id);

            if (toDoItem == null)
            {
                return NotFound();
            }

            return Ok(toDoItem);
        }

        // PUT: api/TodoItems/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutToDoItem([FromRoute] int id, [FromBody] TodoItem toDoItem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != toDoItem.Id)
            {
                return BadRequest();
            }

            _context.Entry(toDoItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ToDoItemExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/TodoItems
        [HttpPost]
        public async Task<IActionResult> PostToDoItem([FromBody] TodoItem toDoItem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.ToDoItem.Add(toDoItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetToDoItem", new { id = toDoItem.Id }, toDoItem);
        }

        // DELETE: api/TodoItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteToDoItem([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var toDoItem = await _context.ToDoItem.SingleOrDefaultAsync(m => m.Id == id);
            if (toDoItem == null)
            {
                return NotFound();
            }

            _context.ToDoItem.Remove(toDoItem);
            await _context.SaveChangesAsync();

            return Ok(toDoItem);
        }

        private bool ToDoItemExists(int id)
        {
            return _context.ToDoItem.Any(e => e.Id == id);
        }
    }
}