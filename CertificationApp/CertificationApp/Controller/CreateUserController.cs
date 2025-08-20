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
    }
}
