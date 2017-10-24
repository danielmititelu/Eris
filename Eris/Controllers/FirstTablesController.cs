using Eris.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eris.Controllers
{
    [Produces("application/json")]
    [Route("api/FirstTables")]
    public class FirstTablesController : Controller
    {
        private readonly ErisDbContext _context;

        public FirstTablesController(ErisDbContext context)
        {
            _context = context;
        }

        // GET: api/FirstTables
        [HttpGet]
        public IEnumerable<FirstTable> GetFirstTable()
        {
            return _context.FirstTable;
        }

        // GET: api/FirstTables/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetFirstTable([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var firstTable = await _context.FirstTable.SingleOrDefaultAsync(m => m.Id == id);

            if (firstTable == null)
            {
                return NotFound();
            }

            return Ok(firstTable);
        }

        // PUT: api/FirstTables/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFirstTable([FromRoute] int id, [FromBody] FirstTable firstTable)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != firstTable.Id)
            {
                return BadRequest();
            }

            _context.Entry(firstTable).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FirstTableExists(id))
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

        // POST: api/FirstTables
        [HttpPost]
        public async Task<IActionResult> PostFirstTable([FromBody] FirstTable firstTable)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.FirstTable.Add(firstTable);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (FirstTableExists(firstTable.Id))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetFirstTable", new { id = firstTable.Id }, firstTable);
        }

        // DELETE: api/FirstTables/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFirstTable([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var firstTable = await _context.FirstTable.SingleOrDefaultAsync(m => m.Id == id);
            if (firstTable == null)
            {
                return NotFound();
            }

            _context.FirstTable.Remove(firstTable);
            await _context.SaveChangesAsync();

            return Ok(firstTable);
        }

        private bool FirstTableExists(int id)
        {
            return _context.FirstTable.Any(e => e.Id == id);
        }
    }
}