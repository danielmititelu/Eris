using Eris.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Eris.Controllers
{
    [Route("api/TodoItems")]
    [Produces("application/json")]
    public class TodoItemsController : Controller
    {
        private readonly ErisDbContext _context;

        public TodoItemsController(ErisDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Return a list of all todo items
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetTodoItems() => Ok(await _context.TodoItem.ToListAsync());

        /// <summary>
        /// Return a todo item with a specific id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTodoItem([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var toDoItem = await _context.TodoItem.SingleOrDefaultAsync(m => m.Id == id);

            if (toDoItem == null)
                return NotFound();

            return Ok(toDoItem);
        }

        /// <summary>
        /// Update a todo item
        /// </summary>
        /// <param name="id"></param>
        /// <param name="todoItem"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTodoItem([FromRoute] int id, [FromBody] TodoItem todoItem)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (id != todoItem.Id)
                return BadRequest();

            _context.Entry(todoItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TodoItemExists(id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }

        /// <summary>
        /// Insert a new todo item
        /// </summary>
        /// <param name="todoItem"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> PostTodoItem([FromBody] TodoItem todoItem)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.TodoItem.Add(todoItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction("api/TodoItems", new { id = todoItem.Id }, todoItem);
        }

        /// <summary>
        /// Delete a todo item
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoItem([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var toDoItem = await _context.TodoItem.SingleOrDefaultAsync(m => m.Id == id);
            if (toDoItem == null)
                return NotFound();

            _context.TodoItem.Remove(toDoItem);
            await _context.SaveChangesAsync();

            return Ok(toDoItem);
        }

        /// <summary>
        /// Check if a todo item with the specified id exists
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private bool TodoItemExists(int id) => _context.TodoItem.Any(e => e.Id == id);
    }
}