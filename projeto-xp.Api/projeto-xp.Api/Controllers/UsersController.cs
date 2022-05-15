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
        private readonly IUserRepository _repository;

        public UsersController(IUserRepository repository)
        {
            _repository = repository;
        }

        // GET: Users
        [HttpGet]
        public async Task<ActionResult<List<UserItemCreate>>> GetUserItems()
        {
            return Ok(await _repository.GetAllUserItems());
        }

        // GET: Users/XXXXXXXX-XXXX-XXXX-XXXX-XXXXXXXXXXXX
        [HttpGet("{id}")]
        public async Task<ActionResult<UserItemCreate>> GetUserItem(string id)
        {
            var userItem = await _repository.GetUserItemById(id);

            if (userItem == null)
            {
                return NotFound();
            }

            return Ok(userItem);
        }

        // PUT: Users/XXXXXXXX-XXXX-XXXX-XXXX-XXXXXXXXXXXX
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserItem(string id, UserItemUpdate userItem)
        {
            if (id == null)
            {
                return BadRequest();
            }
            var ctxUserItem = await _repository.GetUserItemById(id);
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
                await _repository.UpdateUser(ctxUserItem);
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

            return CreatedAtAction(nameof(GetUserItem), new { id = ctxUserItem.Id }, ctxUserItem);
        }

        // POST: Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<UserItemCreate>> PostUserItem(UserItemCreate userItem)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }
            userItem.CreationDate = DateTime.Now;
            userItem.Id = Guid.NewGuid().ToString();
            if (userItem.Surname == null)
            {
                userItem.Surname = "";
            }

            try
            {
                await _repository.AddUser(userItem);
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
            if (id == null)
            {
                return BadRequest();
            }
            var userItem = await _repository.GetUserItemById(id);
            if (userItem == null)
            {
                return NotFound();
            }
            await _repository.DeleteUser(userItem);

            return NoContent();
        }

        private bool UserItemExists(string id)
        {
            return _repository.Exists(id);
        }
    }
}
