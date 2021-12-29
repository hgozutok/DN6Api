using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SaleManager.Core.Entities;
using SaleManager.Core.SaleManagerContext;


namespace SalesManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyUsersController : ControllerBase
    {
        private readonly SaleManagerContext _context;
        //private readonly Microsoft.AspNetCore.Identity.UserManager<ApplicationUser> _userManager;

        public CompanyUsersController(SaleManagerContext context
            //Microsoft.AspNetCore.Identity.UserManager<ApplicationUser> userManager
            )
        {
            _context = context;
             //_userManager =  userManager; 
        }

        // GET: api/CompanyUsers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CompanyUsers>>> GetCompanyUsers()
        {
            return await _context.CompanyUsers.Include(y => y.Company).ToListAsync();
        }

        // GET: api/CompanyUsers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CompanyUsers>> GetCompanyUsers(string id)
        {
            //var user = await _userManager.FindByEmailAsync("hugo@gozutok.info");
            //var claims = await _userManager.GenerateUserTokenAsync(user,"MY SPA","login");
            //var token = await _userManager.GetAuthenticationTokenAsync(user, "My SPA", null);
            var companyUsers = await _context.CompanyUsers.Where(x=>x.UserName== id)
                                            .Include(y=>y.Company).FirstOrDefaultAsync();

            if (companyUsers == null)
            {
                return NotFound();
            }

            return companyUsers;
        }

        // PUT: api/CompanyUsers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCompanyUsers(int id, CompanyUsers companyUsers)
        {
            if (id != companyUsers.ID)
            {
                return BadRequest();
            }

            _context.Entry(companyUsers).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CompanyUsersExists(id))
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

        // POST: api/CompanyUsers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CompanyUsers>> PostCompanyUsers(CompanyUsers companyUsers)
        {
            _context.CompanyUsers.Add(companyUsers);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCompanyUsers", new { id = companyUsers.ID }, companyUsers);
        }

        // DELETE: api/CompanyUsers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCompanyUsers(int id)
        {
            var companyUsers = await _context.CompanyUsers.FindAsync(id);
            if (companyUsers == null)
            {
                return NotFound();
            }

            _context.CompanyUsers.Remove(companyUsers);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CompanyUsersExists(int id)
        {
            return _context.CompanyUsers.Any(e => e.ID == id);
        }
    }
}
