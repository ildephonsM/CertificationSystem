using CertificationApp.Data;
using CertificationApp.Shared.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CertificationApp.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VenueController : ControllerBase
    {
        private readonly CertificationDbContext _context;

        public VenueController(CertificationDbContext context)
        {
            _context = context;
        }

        // GET: api/Venue
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Venues>>> GetVenues()
        {
            return await _context.Venues
                                 .Include(v => v.Course) // Include course info if needed
                                 .ToListAsync();
        }

        // GET: api/Venue/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Venues>> GetVenue(Guid id)
        {
            var venue = await _context.Venues.FindAsync(id);

            if (venue == null)
                return NotFound();

            return venue;
        }

        // POST: api/Venue
        [HttpPost]
        public async Task<ActionResult<Venues>> CreateVenue(Venues venue)
        {
            venue.Id = Guid.NewGuid(); // Generate a new GUID
            _context.Venues.Add(venue);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetVenue), new { id = venue.Id }, venue);
        }

        // PUT: api/Venue/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateVenue(Guid id, Venues updatedVenue)
        {
            if (id != updatedVenue.Id)
                return BadRequest();

            var venue = await _context.Venues.FindAsync(id);
            if (venue == null)
                return NotFound();

            venue.TraineeName = updatedVenue.TraineeName;
            venue.CourseDate = updatedVenue.CourseDate;
            venue.CourseId = updatedVenue.CourseId;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/Venue/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVenue(Guid id)
        {
            var venue = await _context.Venues.FindAsync(id);
            if (venue == null)
                return NotFound();

            _context.Venues.Remove(venue);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
