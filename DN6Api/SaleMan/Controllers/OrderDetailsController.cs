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
    public class OrderDetailsController : ControllerBase
    {
        private readonly SaleManagerContext _context;

        public OrderDetailsController(SaleManagerContext context)
        {
            _context = context;
        }

        // GET: api/OrderDetails
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order_Details>>> GetOrder_Details()
        {
            return await _context.Order_Details.Include(p => p.Product).ToListAsync();
        }

        // GET: api/OrderDetails/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Order_Details>> GetOrder_Details(int id)
        {
            var order_Details = await _context.Order_Details.Include(p => p.Product)
                .Where(y => y.OrderID == id).FirstOrDefaultAsync();


            if (order_Details == null)
            {
                return NotFound();
            }

            return order_Details;
        }

        // PUT: api/OrderDetails/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrder_Details(int id, Order_Details order_Details)
        {
            if (id != order_Details.Id)
            {
                return BadRequest();
            }

            _context.Entry(order_Details).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Order_DetailsExists(id))
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

        // POST: api/OrderDetails
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Order_Details>> PostOrder_Details(Order_Details order_Details)
        {
            _context.Order_Details.Add(order_Details);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOrder_Details", new { id = order_Details.Id }, order_Details);
        }

        // DELETE: api/OrderDetails/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder_Details(int id)
        {
            var order_Details = await _context.Order_Details.FindAsync(id);
            if (order_Details == null)
            {
                return NotFound();
            }

            _context.Order_Details.Remove(order_Details);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool Order_DetailsExists(int id)
        {
            return _context.Order_Details.Any(e => e.Id == id);
        }
    }
}
