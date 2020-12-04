using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CmdApi.Models;
using Microsoft.AspNetCore.Cors;

namespace CmdApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoUsersController : ControllerBase
    {
        private readonly CommandContext _context;

        public TodoUsersController(CommandContext context)
        {
            _context = context;
        }

        // GET: api/TodoUsers
        [EnableCors("MyPolicy")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TblUsers>>> GetTblUsers()
        {
            return await _context.TblUsers.ToListAsync();
        }

        // GET: api/TodoUsers/5
        [EnableCors("MyPolicy")]
        [HttpGet("{id}")]
        public async Task<ActionResult<TblUsers>> GetUsers(int id)
        {
            var users = await _context.TblUsers.FindAsync(id);

            if (users == null)
            {
                return NotFound();
            }

            return users;
        }

        // PUT: api/TodoUsers/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [EnableCors("MyPolicy")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsers(int id, TblUsers users)
        {
            if (id != users.Id)
            {
                return BadRequest();
            }

            _context.Entry(users).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsersExists(id))
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

        // POST: api/TodoUsers
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [DisableCors]
        [HttpPost]
        public async Task<ActionResult<TblUsers>> PostUsers(Login login)
        {
            TblUsers user =  _context.TblUsers.Where(x=>x.UserName == login.UserName && x.Password == login.Password).FirstOrDefault();
            
            if(user != null){
                return Ok(user);
            }
            return NotFound();
        }

        // DELETE: api/TodoUsers/5
        [EnableCors("MyPolicy")]
        [HttpDelete("{id}")]
        public async Task<ActionResult<TblUsers>> DeleteUsers(int id)
        {
            var users = await _context.TblUsers.FindAsync(id);
            if (users == null)
            {
                return NotFound();
            }

            _context.TblUsers.Remove(users);
            await _context.SaveChangesAsync();

            return users;
        }

        private bool UsersExists(int id)
        {
            return _context.TblUsers.Any(e => e.Id == id);
        }
    }
}
