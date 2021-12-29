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
    public class SparePartsController : ControllerBase
    {
        private readonly SaleManagerContext _context;

        public SparePartsController(SaleManagerContext context)
        {
            _context = context;
        }

        // GET: api/SpareParts
        [HttpGet("{companyID}")]
        //[Route("[controller]/{companyID}")]
        public async Task<ActionResult<IEnumerable<SpareParts>>> GetSpareParts(int companyID)
        {
            return await _context.SpareParts.Where(c => c.CompanyID == companyID).ToListAsync();
        }

        // GET: api/SpareParts/5
        [HttpGet("{companyID}/{id}")]
        //[HttpGet]
        //[Route("[controller]/{companyID}/{id}")]

        public async Task<ActionResult<SpareParts>> GetSpareParts(int companyID, int id)
        {
            var spareParts = await _context.SpareParts.Where(y => y.SpareID == id && y.CompanyID == companyID)
              .FirstOrDefaultAsync();

            if (spareParts == null)
            {
                return NotFound();
            }

            return spareParts;
        }

        // PUT: api/SpareParts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{companyID}/{id}")]
        public async Task<IActionResult> PutSpareParts(int companyID, int id, SpareParts spareParts)
        {
            if (id != spareParts.SpareID || companyID != spareParts.CompanyID)
            {
                return BadRequest();
            }

            _context.Entry(spareParts).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SparePartsExists(id))
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

        // POST: api/SpareParts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("{companyID}")]
        public async Task<ActionResult<SpareParts>> PostSpareParts(int companyID, SpareParts spareParts)
        {
            if (companyID != spareParts.CompanyID)
            {
                return BadRequest();
            }
            _context.SpareParts.Add(spareParts);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSpareParts", new { id = spareParts.SpareID }, spareParts);
        }

        // DELETE: api/SpareParts/5
        [HttpDelete("{companyID}/{id}")]
        public async Task<IActionResult> DeleteSpareParts(int companyID, int id)
        {
            var spareParts = await _context.SpareParts.FindAsync(id);
            if (spareParts == null)
            {
                return NotFound();
            }

            _context.SpareParts.Remove(spareParts);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SparePartsExists(int id)
        {
            return _context.SpareParts.Any(e => e.SpareID == id);
        }
    }
}
