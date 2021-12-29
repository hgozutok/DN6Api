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
    public class ProductsController : ControllerBase
    {
        private readonly SaleManagerContext _context;

        public ProductsController(SaleManagerContext context)
        {
            _context = context;
        }

        // GET: api/Products
        [HttpGet("{companyID}")]
        //[Route("[controller]/{companyID}")]
        //  [HttpGet("{id}/{companyID}")]
        public async Task<ActionResult<IEnumerable<Products>>> GetProducts(int companyID)
        {
            return await _context.Products.Where(c => c.CompanyID == companyID).Include(x => x.Category).Include(y => y.ProductParts).ThenInclude(z => z.Spare).ToListAsync();
        }

        // GET: api/Products/5
        [HttpGet("{companyID}/{id}")]
        //[HttpGet]
        //[Route("[controller]/{companyID}/{id}")]
        public async Task<ActionResult<Products>> GetProducts(int id, int companyID)
        {
            var products = await _context.Products.Where(y => y.ProductID == id && y.CompanyID == companyID)

               // var orders = await _context.Orders.Where(y => y.OrderID == id)
               .Include(x => x.ProductParts)
               .ThenInclude(y => y.Spare)

               .FirstOrDefaultAsync();



            if (products == null)
            {
                return NotFound();
            }

            return products;
        }

        // PUT: api/Products/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{companyID}/{id}")]
        public async Task<IActionResult> PutProducts(int companyID, int id, Products products)
        {
            if (id != products.ProductID || companyID != products.CompanyID)
            {
                return BadRequest();
            }

            _context.Entry(products).State = EntityState.Modified;

            try
            {
                _context.Products.Update(products);

                List<int> previousIds = await _context.ProductParts.Where(x => x.ProductID == products.ProductID)
                    .Select(t => t.ID).ToListAsync();
                List<int> currentIds = products.ProductParts.Select(t => t.ID).ToList();
                List<int> deletedIds = previousIds
                    .Except(currentIds).ToList();

                foreach (var del in deletedIds)
                {
                    ProductParts odet = _context.ProductParts.Single(ox => ox.ProductID == products.ProductID
                                         && ox.ID == del);
                    _context.Entry(odet).State = EntityState.Deleted;
                    _context.ProductParts.Remove(odet);
                    await _context.SaveChangesAsync();
                }



                foreach (var item in products.ProductParts)
                {

                    if (item.ID == 0)
                    {
                        item.ProductID = products.ProductID;
                        _context.Entry(item).State = EntityState.Added;
                        _context.ProductParts.Add(item);
                        await _context.SaveChangesAsync();

                    }
                    else
                    {
                        _context.Entry(item).State = EntityState.Modified;
                        _context.ProductParts.Update(item);
                        await _context.SaveChangesAsync();
                    }

                }
                await _context.SaveChangesAsync();

            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductsExists(id))
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

        // POST: api/Products
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("{companyID}")]
        public async Task<ActionResult<Products>> PostProducts(int companyID, Products products)
        {
            if (companyID != products.CompanyID)
            {
                return BadRequest();
            }
            _context.Products.Add(products);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProducts", new { id = products.ProductID }, products);
        }

        // DELETE: api/Products/5
        [HttpDelete("{companyID}/{id}")]
        public async Task<IActionResult> DeleteProducts(int companyID, int id)
        {



            var products = await _context.Products.FindAsync(id);
            if (products == null)
            {
                return NotFound();
            }

            _context.Products.Remove(products);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductsExists(int id)
        {
            return _context.Products.Any(e => e.ProductID == id);
        }
    }
}
