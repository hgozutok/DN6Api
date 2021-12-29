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
    public class OrderWithCustomersController : ControllerBase
    {
        private readonly SaleManagerContext _context;

        public OrderWithCustomersController(SaleManagerContext context)
        {
            _context = context;
        }

        // GET: api/orderswithcustomers
        [HttpGet]
        //public async Task<ActionResult<IQueryable<Orders>>> GetOrders()
        public async Task<IQueryable<Orders>> GetOrderWithCustomers()
        {
            var q = from customer in _context.Customers
                    join order in _context.Orders on customer.CustomerID equals order.CustomerID
                    select new
                    {
                        customer.CompanyName,
                        customer.CustomerID,
                        customer.ContactName,
                        order.EmployeeID,
                        order.Approved,
                        order.OrderID,
                        order.OrderName,
                        order.OrderDate
                    };

            return (IQueryable<Orders>)await q.ToListAsync();
            //await _context.Orders 
            //.Include(x => x.Customer)                  
            // .ToListAsync();
        }

        // GET: api/Orders/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Orders>> GetOrders(int id)
        {
            //var orders = await _context.Orders.Where(y => y.OrderID == id)
            //    .Include(x => x.Order_Details
            //    .Select(q => q.Product)
            //    )
            //    .FirstOrDefaultAsync();
            var orders = await _context.Orders.Where(y => y.OrderID == id)
                .Include(x => x.Order_Details)
                .ThenInclude(y => y.Product)



                .FirstOrDefaultAsync();

            if (orders == null)
            {
                return NotFound();
            }

            return orders;
        }

        // PUT: api/Orders/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrders(int id, Orders orders)
        {
            if (id != orders.OrderID)
            {
                return BadRequest();
            }

            _context.Entry(orders).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrdersExists(id))
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

        // POST: api/Orders
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Orders>> PostOrders(Orders orders)
        {
            _context.Orders.Add(orders);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOrders", new { id = orders.OrderID }, orders);
        }

        // DELETE: api/Orders/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrders(int id)
        {
            var orders = await _context.Orders.FindAsync(id);
            if (orders == null)
            {
                return NotFound();
            }

            _context.Orders.Remove(orders);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OrdersExists(int id)
        {
            return _context.Orders.Any(e => e.OrderID == id);
        }
    }
}
