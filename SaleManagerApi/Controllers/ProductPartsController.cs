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
    public class ProductPartsController : ControllerBase
    {
        private readonly SaleManagerContext _context;

        public ProductPartsController(SaleManagerContext context)
        {
            _context = context;
        }

        // GET: api/ProductParts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductParts>>> GetProductParts()
        {
            return await _context.ProductParts.ToListAsync();
        }

        // GET: api/ProductParts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductParts>> GetProductParts(int id)
        {
            var productParts = await _context.ProductParts.FindAsync(id);

            if (productParts == null)
            {
                return NotFound();
            }

            return productParts;
        }

        // PUT: api/ProductParts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProductParts(int id, ProductParts productParts)
        {
            if (id != productParts.ID)
            {
                return BadRequest();
            }

            _context.Entry(productParts).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductPartsExists(id))
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

        // POST: api/ProductParts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ProductParts>> PostProductParts(ProductParts productParts)
        {
            _context.ProductParts.Add(productParts);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProductParts", new { id = productParts.ID }, productParts);
        }

        // DELETE: api/ProductParts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductParts(int id)
        {
            var productParts = await _context.ProductParts.FindAsync(id);
            if (productParts == null)
            {
                return NotFound();
            }

            _context.ProductParts.Remove(productParts);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductPartsExists(int id)
        {
            return _context.ProductParts.Any(e => e.ID == id);
        }
    }
}
