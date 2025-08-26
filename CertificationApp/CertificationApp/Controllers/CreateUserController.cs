using CertificationApp.Data;
using CertificationApp.Shared.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CertificationApp.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CertificatesController : ControllerBase
    {
        private readonly CertificationDbContext _context;

        public CertificatesController(CertificationDbContext context)
        {
            _context = context;
        }

        // GET: api/Certificates
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CreateUser>>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }

        // POST: api/Certificates
        [HttpPost]
        public async Task<ActionResult<CreateUser>> CreateUser(CreateUser user)
        {
            // Check duplicate ID
            if (await _context.Users.AnyAsync(c => c.IdNumber == user.IdNumber))
            {
                return BadRequest("A certificate with this ID number already exists.");
            }

            user.CreatedDate = DateTime.Now;
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetUsers), new { id = user.Id }, user);
        }

        // PUT: api/Certificate/{id)
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(Guid id, CreateUser updatedUser)
        {
            if (id != updatedUser.Id)
                return BadRequest();

            var user = await _context.Users.FindAsync(id);
            if (user == null)
                return NotFound();

            // Only allow editing Name & Surname
            user.Name = updatedUser.Name;
            user.Surname = updatedUser.Surname;

            user.DateModified = DateTime.Now;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/Certificates/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
                return NotFound();

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
