using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SaleManager.Core.Entities;
using SaleManager.Core.SaleManagerContext;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesMan.Controllers
{
 //   [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly SaleManagerContext _context;

        public OrdersController(SaleManagerContext context)
        {
            _context = context;
        }

        // GET: api/Orders
        [HttpGet("{companyID}")]
        public async Task<ActionResult<IEnumerable<Orders>>> GetOrders(int companyID)
        //        public async Task<IQueryable<Orders>> GetOrders()
        {
            //var q = from c in categories
            //        join p in products on c equals p.Category into ps
            //        from p in ps.DefaultIfEmpty()
            //        select (Category: c, ProductName: p == null ? "(No products)" : p.ProductName);

            //var result = (from p in Products
            //              join o in Orders on p.ProductId equals o.ProductId
            //              join c in Customers on o.CustomerId equals c.CustomerId
            //              select new
            //              {
            //                  o.OrderId,
            //                  o.OrderNumber,
            //                  p.ProductName,
            //                  o.Quantity,
            //              });


            //left outter

            //    var result = (from p in Products
            //                  join o in Orders on p.ProductId equals o.ProductId into temp
            //                  from t in temp.DefaultIfEmpty()
            //                  select new
            //                  {
            //                      p.ProductId,
            //                      OrderId = (int?)t.OrderId,
            //                      t.OrderNumber,
            //                      p.ProductName,
            //                      Quantity = (int?)t.Quantity,
            //                      t.TotalAmount,
            //                      t.OrderDate
            //                  });

            //var q = from customer  in _context.Customers
            //        join order in _context.Orders on customer.CustomerID equals order.CustomerID
            //        select new
            //        {
            //            customer.CompanyName,
            //            customer.CustomerID,
            //            customer.ContactName,
            //            order.EmployeeID,
            //            order.Approved,
            //            order.OrderID,
            //            order.OrderName,
            //            order.OrderDate

            //        };
            // (IQueryable<Orders>)await q.ToListAsync();
            return await _context.Orders.Where(c => c.CompanyID == companyID)
            .Include(x => x.Customer)

             .ToListAsync();
        }

        // GET: api/Orders/5
        [HttpGet("{companyID}/{id}")]
        public async Task<ActionResult<Orders>> GetOrders(int companyID, int id)
        {
            //var orders = await _context.Orders.Where(y => y.OrderID == id)
            //    .Include(x => x.Order_Details
            //    .Select(q => q.Product)
            //    )
            //    .FirstOrDefaultAsync();
            var orders = await _context.Orders.Where(y => y.CompanyID == companyID && y.OrderID == id)
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
        [HttpPut("{companyID}/{id}")]
        public async Task<IActionResult> PutOrders(int companyID, int id, Orders orders)
        {
            if (id != orders.OrderID || companyID != orders.CompanyID)
            {
                return BadRequest();
            }

            _context.Entry(orders).State = EntityState.Modified;

            try
            {
                _context.Orders.Update(orders);

                List<int> previousIds = await _context.Order_Details.Where(x => x.OrderID == orders.OrderID)
                    .Select(t => t.Id).ToListAsync();
                List<int> currentIds = orders.Order_Details.Select(t => t.Id).ToList();
                List<int> deletedIds = previousIds
                    .Except(currentIds).ToList();

                foreach (var del in deletedIds)
                {
                    Order_Details odet = _context.Order_Details.Single(ox => ox.OrderID == orders.OrderID
                                         && ox.Id == del);
                    _context.Entry(odet).State = EntityState.Deleted;
                    _context.Order_Details.Remove(odet);
                    await _context.SaveChangesAsync();
                }



                foreach (var item in orders.Order_Details)
                {

                    if (item.Id == 0)
                    {
                        item.OrderID = orders.OrderID;
                        _context.Entry(item).State = EntityState.Added;
                        _context.Order_Details.Add(item);
                        await _context.SaveChangesAsync();

                    }
                    else
                    {
                        _context.Entry(item).State = EntityState.Modified;
                        _context.Order_Details.Update(item);
                        await _context.SaveChangesAsync();
                    }

                }
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
        [HttpPost("{companyID}")]
        public async Task<ActionResult<Orders>> PostOrders(int companyID, Orders orders)
        {
            if (companyID != orders.CompanyID)
            {
                return BadRequest();
            }
            _context.Orders.Add(orders);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOrders", new { id = orders.OrderID }, orders);
        }

        // DELETE: api/Orders/5
        [HttpDelete("{companyID}/{id}")]
        public async Task<IActionResult> DeleteOrders(int companyID, int id)
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
