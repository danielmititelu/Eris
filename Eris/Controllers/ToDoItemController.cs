using Eris.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Eris.Controllers
{
    [Produces("application/json")]
    [Route("api/Task")]
    public class ToDoItemController : Controller
    {
        private readonly ErisDbContext _context;

        public ToDoItemController(ErisDbContext context)
        {
            _context = context;
        }

        // GET: api/Tasks
        [HttpGet]
        public IActionResult GetTasks()
        {
            return Ok(_context.ToDoItem);
        }

        // GET: api/Task/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTask([FromRoute] int id)
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

        // PUT: api/Task/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTask([FromRoute] int id, [FromBody] ToDoItem toDoItem)
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
                if (!TaskExists(id))
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

        // POST: api/Task
        [HttpPost]
        public async Task<IActionResult> PostTask([FromBody] ToDoItem toDoItem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.ToDoItem.Add(toDoItem);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (TaskExists(toDoItem.Id))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetTask", new { id = toDoItem.Id }, toDoItem);
        }

        // DELETE: api/Task/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var toDoitem = await _context.ToDoItem.SingleOrDefaultAsync(m => m.Id == id);
            if (toDoitem == null)
            {
                return NotFound();
            }

            _context.ToDoItem.Remove(toDoitem);
            await _context.SaveChangesAsync();

            return Ok(toDoitem);
        }

        private bool TaskExists(int id)
        {
            return _context.ToDoItem.Any(e => e.Id == id);
        }
    }
}