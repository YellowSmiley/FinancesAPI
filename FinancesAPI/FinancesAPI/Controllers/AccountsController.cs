using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FinancesAPI.Models;

namespace FinancesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly AccountsContext _context;

        public AccountsController(AccountsContext context)
        {
            _context = context;
        }

        // GET: api/Accounts
        [HttpGet]
        public IEnumerable<Account> GetAccounts()
        {
            return _context.Accounts;
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
    }
}