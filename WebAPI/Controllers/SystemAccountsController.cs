using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessObjects;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Services;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SystemAccountsController : ControllerBase
    {
        private readonly ISystemAccountService _context;

        public SystemAccountsController(ISystemAccountService context)
        {
            _context = context;
        }

        // GET: api/SystemAccounts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SystemAccount>>> GetSystemAccounts()
        {
            var accounts = await Task.FromResult(_context.GetAccounts());
            return Ok(accounts);
        }

        // GET: api/SystemAccounts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SystemAccount>> GetSystemAccount(int id)
        {
            var systemAccount = await Task.FromResult(_context.GetAccountByID(id));
            if (systemAccount == null)
            {
                return NotFound();
            }
            return Ok(systemAccount);

        }

        // PUT: api/SystemAccounts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSystemAccount(short id, SystemAccount systemAccount)
        {
            if (id != systemAccount.AccountId)
            {
                return BadRequest("Account ID mismatch.");
            }

            var existingAccount = await Task.FromResult(_context.GetAccountByID(id));
            if (existingAccount == null)
            {
                return NotFound();
            }

            try
            {
                _context.UpdateAccount(id, systemAccount);
                // If your service is async, await it here.
                return Ok(new {message = "update successful"});
            }
            catch (Exception ex)
            {
                // Optionally log the exception
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // POST: api/SystemAccounts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<SystemAccount>> PostSystemAccount(SystemAccount systemAccount)
        {
            try
            {
                // Check if account already exists
                var existingAccount = await Task.FromResult(_context.GetAccountByID(systemAccount.AccountId));
                if (existingAccount != null)
                {
                    return Conflict("Account already exists.");
                }

                _context.AddAccount(systemAccount);
                // If your service is async, await it here

                return CreatedAtAction(nameof(GetSystemAccount), new { id = systemAccount.AccountId }, systemAccount);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        // DELETE: api/SystemAccounts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSystemAccount(short id)
        {
            var systemAccount = await Task.FromResult(_context.GetAccountByID(id));
            if (systemAccount == null)
            {
                return NotFound();
            }

            try
            {
                _context.DeleteAccount(id);
                // If your service is async, await it here

                return Ok(new { message = "delete successful" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
