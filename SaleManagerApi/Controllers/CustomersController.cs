using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SaleManager.Core.Entities;
using SaleManager.Core.SaleManagerContext;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesMan.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly SaleManagerContext _context;

        public CustomersController(SaleManagerContext context)
        {
            _context = context;
        }

        // GET: api/Customers
        [HttpGet("{companyID}")]
        public async Task<ActionResult<IEnumerable<Customers>>> GetCustomers(int companyID)
        {
            return await _context.Customers.Where(c => c.CompanyID == companyID).ToListAsync();
        }

        // GET: api/Customers/5
        [HttpGet("{companyID}/{id}")]
        public async Task<ActionResult<Customers>> GetCustomers(int companyID, int id)
        {
            var customers = await _context.Customers.Where(y => y.CompanyID == companyID && y.CustomerID == id).Include(x => x.Orders).FirstOrDefaultAsync();

            if (customers == null)
            {
                return NotFound();
            }

            return customers;
        }

        // PUT: api/Customers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{companyID}/{id}")]
        public async Task<IActionResult> PutCustomers(int companyID, int id, Customers customers)
        {
            if (id != customers.CustomerID)
            {
                return BadRequest();
            }

            _context.Entry(customers).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomersExists(id))
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

        // POST: api/Customers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("{companyID}")]
        public async Task<ActionResult<Customers>> PostCustomers(int companyID, Customers customers)
        {
            if (companyID != customers.CompanyID)
            {
                return BadRequest();
            }
            _context.Customers.Add(customers);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCustomers", new { id = customers.CustomerID }, customers);
        }

        // DELETE: api/Customers/5
        [HttpDelete("{companyID}/{id}")]
        public async Task<IActionResult> DeleteCustomers(int companyID, int id)
        {
            var customers = await _context.Customers.FindAsync(id);
            if (customers == null)
            {
                return NotFound();
            }

            _context.Customers.Remove(customers);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CustomersExists(int id)
        {
            return _context.Customers.Any(e => e.CustomerID == id);
        }
        //[HttpGet("{CompanyName}")]
        //public async Task<ActionResult<IEnumerable<Customers>>> CustomersExists( string name)

        //{
        //    return await _context.Customers.Where(e =>  e.CompanyName == name).ToListAsync();
        //}
    }
}
