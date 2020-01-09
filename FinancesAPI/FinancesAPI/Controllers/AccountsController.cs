using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FinancesAPI.Models;

namespace FinancesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly AccountContext _context;

        public AccountsController(AccountContext context)
        {
            _context = context;
        }

        // GET: api/Accounts
        [HttpGet]
        public IEnumerable<Account> GetAccounts()
        {
            return _context.Accounts;
        }

        // GET: api/Accounts/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAccount([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var account = await _context.Accounts.FindAsync(id);

            if (account == null)
            {
                return NotFound();
            }

            return Ok(account);
        }

        // PUT: api/Accounts/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAccount([FromRoute] long id, [FromBody] Account account)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != account.Id)
            {
                return BadRequest();
            }

            _context.Entry(account).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AccountExists(id))
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

        // POST: api/Accounts
        [HttpPost]
        public async Task<IActionResult> PostAccount([FromBody] IList<Account> accounts)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Delete
            if (_context.Accounts.Count() > accounts.Count())
            {
                foreach (Account account in _context.Accounts)
                {
                    var existingAccount = accounts.Where(acc => acc.Id == account.Id);
                    if (!existingAccount.Any())
                    {
                        _context.Remove(_context.Accounts.Single(a => a.Id == account.Id));
                    }
                }
            }

            foreach (Account account in accounts)
            {
                var existingAccount = _context.Accounts.Where(acc => acc.Id == account.Id);
                // Add
                if (!existingAccount.Any())
                {
                    _context.Accounts.Add(account);
                }
                // Modify
                else
                {
                    var ea = _context.Accounts.First(acc => acc.Id == account.Id);
                    ea = account;
                }
            }

            await _context.SaveChangesAsync();


             return Ok(_context.Accounts);
        }

        // DELETE: api/Accounts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAccount([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var account = await _context.Accounts.FindAsync(id);
            if (account == null)
            {
                return NotFound();
            }

            _context.Accounts.Remove(account);
            await _context.SaveChangesAsync();

            return Ok(account);
        }

        private bool AccountExists(long id)
        {
            return _context.Accounts.Any(e => e.Id == id);
        }
    }
}