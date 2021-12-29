using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SaleManager.Core.Entities;
using SaleManager.Core.SaleManagerContext;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShippersController : ControllerBase
    {
        private readonly SaleManagerContext _context;

        public ShippersController(SaleManagerContext context)
        {
            _context = context;
        }

        // GET: api/Shippers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Shippers>>> GetShippers()
        {
            return await _context.Shippers.ToListAsync();
        }

        // GET: api/Shippers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Shippers>> GetShippers(int id)
        {
            var shippers = await _context.Shippers.FindAsync(id);

            if (shippers == null)
            {
                return NotFound();
            }

            return shippers;
        }

        // PUT: api/Shippers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutShippers(int id, Shippers shippers)
        {
            if (id != shippers.ShipperID)
            {
                return BadRequest();
            }

            _context.Entry(shippers).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ShippersExists(id))
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

        // POST: api/Shippers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Shippers>> PostShippers(Shippers shippers)
        {
            _context.Shippers.Add(shippers);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetShippers", new { id = shippers.ShipperID }, shippers);
        }

        // DELETE: api/Shippers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteShippers(int id)
        {
            var shippers = await _context.Shippers.FindAsync(id);
            if (shippers == null)
            {
                return NotFound();
            }

            _context.Shippers.Remove(shippers);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ShippersExists(int id)
        {
            return _context.Shippers.Any(e => e.ShipperID == id);
        }
    }
}
