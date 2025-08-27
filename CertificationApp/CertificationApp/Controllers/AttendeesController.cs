using CertificationApp.Data;
using CertificationApp.Shared.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CertificationApp.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttendeesController : ControllerBase
    {
        private readonly CertificationDbContext _context;

        public AttendeesController(CertificationDbContext context)
        {
            _context = context;
        }

        // GET: api/Attendees
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Attendees>>> GetAttendees()
        {
            return await _context.Attendees.Include(a => a.Venues).ToListAsync();
        }

        // POST: api/Attendess
        [HttpPost]
        public async Task<ActionResult<Attendees>> CreateAttendee (Guid userId, Guid venueId)
        {
            var user = await _context.Users.FindAsync(userId);
            var venue = await _context.Venues.FindAsync(venueId);

            if (user == null || venue == null)
                return BadRequest("Invalid user or venue selected");

            var attendee = new Attendees
            {
                Id = Guid.NewGuid(),
                Name = user.Name,
                Surname = user.Surname,
                IdNumber = user.IdNumber,
                VenueId = venue.Id,
                DateCreated = DateTime.Now,
            };

            _context.Attendees.Add(attendee);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAttendees), new { id = attendee.Id }, attendee);
        }
    }
}
