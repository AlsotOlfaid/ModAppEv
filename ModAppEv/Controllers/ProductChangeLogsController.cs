using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ModAppEv.Data;
using ModAppEv.Models;

namespace ModAppEv.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductChangeLogsController : ControllerBase
    {
        private readonly ModelationDBContext _context;

        public ProductChangeLogsController(ModelationDBContext context)
        {
            _context = context;
        }

        // GET: api/ProductChangeLogs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductChangeLog>>> GetProductChangeLogs()
        {
            return await _context.ProductChangeLogs.ToListAsync();
        }

        // GET: api/ProductChangeLogs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductChangeLog>> GetProductChangeLog(int id)
        {
            var productChangeLog = await _context.ProductChangeLogs.FindAsync(id);

            if (productChangeLog == null)
            {
                return NotFound();
            }

            return productChangeLog;
        }

        // PUT: api/ProductChangeLogs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProductChangeLog(int id, ProductChangeLog productChangeLog)
        {
            if (id != productChangeLog.Id)
            {
                return BadRequest();
            }

            _context.Entry(productChangeLog).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductChangeLogExists(id))
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

        // POST: api/ProductChangeLogs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ProductChangeLog>> PostProductChangeLog(ProductChangeLog productChangeLog)
        {
            _context.ProductChangeLogs.Add(productChangeLog);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProductChangeLog", new { id = productChangeLog.Id }, productChangeLog);
        }

        // DELETE: api/ProductChangeLogs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductChangeLog(int id)
        {
            var productChangeLog = await _context.ProductChangeLogs.FindAsync(id);
            if (productChangeLog == null)
            {
                return NotFound();
            }

            _context.ProductChangeLogs.Remove(productChangeLog);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductChangeLogExists(int id)
        {
            return _context.ProductChangeLogs.Any(e => e.Id == id);
        }
    }
}
