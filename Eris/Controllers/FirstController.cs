using Eris.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eris.Controllers
{
    [Produces("application/json")]
    [Route("api/First")]
    public class FirstController : Controller
    {
        private readonly FirstTableContext _context;

        public FirstController(FirstTableContext context)
        {
            _context = context;
        }

        // GET: api/First
        [HttpGet]
        public IEnumerable<FirstTable> GetFirstTables()
        {
            return _context.FirstTables;
        }

        // GET: api/First/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetFirstTable([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var firstTable = await _context.FirstTables.SingleOrDefaultAsync(m => m.Id == id);

            if (firstTable == null)
            {
                return NotFound();
            }

            return Ok(firstTable);
        }

        // PUT: api/First/5
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

        // POST: api/First
        [HttpPost]
        public async Task<IActionResult> PostFirstTable([FromBody] FirstTable firstTable)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.FirstTables.Add(firstTable);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFirstTable", new { id = firstTable.Id }, firstTable);
        }

        // DELETE: api/First/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFirstTable([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var firstTable = await _context.FirstTables.SingleOrDefaultAsync(m => m.Id == id);
            if (firstTable == null)
            {
                return NotFound();
            }

            _context.FirstTables.Remove(firstTable);
            await _context.SaveChangesAsync();

            return Ok(firstTable);
        }

        private bool FirstTableExists(int id)
        {
            return _context.FirstTables.Any(e => e.Id == id);
        }
    }
}