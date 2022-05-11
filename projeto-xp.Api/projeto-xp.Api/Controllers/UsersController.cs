#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using projeto_xp.Models;

namespace projeto_xp.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserContext _context;

        public UsersController(UserContext context)
        {
            _context = context;
        }

        // GET: Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserItemCreate>>> GetUserItems()
        {
            return await _context.UserItems.ToListAsync();
        }

        // GET: Users/XXXXXXXX-XXXX-XXXX-XXXX-XXXXXXXXXXXX
        [HttpGet("{id}")]
        public async Task<ActionResult<UserItemCreate>> GetUserItem(string id)
        {
            var userItem = await _context.UserItems.FindAsync(id);

            if (userItem == null)
            {
                return NotFound();
            }

            return userItem;
        }

        // PUT: Users/XXXXXXXX-XXXX-XXXX-XXXX-XXXXXXXXXXXX
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserItem(string id, UserItemUpdate userItem)
        {
            var ctxUserItem = await _context.UserItems.FindAsync(id);
            if (ctxUserItem == null)
            {
                return BadRequest();
            }

            if(userItem.Name != null)
            {
                ctxUserItem.Name = userItem.Name;
            }
            if(userItem.Surname != null)
            {
                ctxUserItem.Surname = userItem.Surname;
            }
            if(userItem.Age != null)
            {
                ctxUserItem.Age = userItem.Age;
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserItemExists(id))
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

        // POST: Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<UserItemCreate>> PostUserItem(UserItemCreate userItem)
        {
            userItem.CreationDate = DateTime.Now;
            userItem.Id = Guid.NewGuid().ToString();
            if (userItem.Surname == null)
            {
                userItem.Surname = "";
            }

            _context.UserItems.Add(userItem);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (UserItemExists(userItem.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction(nameof(GetUserItem), new { id = userItem.Id }, userItem);
        }

        // DELETE: Users/XXXXXXXX-XXXX-XXXX-XXXX-XXXXXXXXXXXX
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserItem(string id)
        {
            var userItem = await _context.UserItems.FindAsync(id);
            if (userItem == null)
            {
                return NotFound();
            }

            _context.UserItems.Remove(userItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserItemExists(string id)
        {
            return _context.UserItems.Any(e => e.Id == id);
        }
    }
}
