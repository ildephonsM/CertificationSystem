using CertificationApp.Data;
using CertificationApp.Shared.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CertificationApp.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private readonly CertificationDbContext _context;

        public CoursesController(CertificationDbContext context)
        {
            _context = context;
        }

        // GET: api/Courses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Courses>>> GetCourses()
        {
            return await _context.Courses.ToListAsync();
        }

        // POST: api/Courses
        [HttpPost]
        public async Task<ActionResult<Courses>> PostCourse(Courses course)
        {
            // Optional: check if course already exists
            if (_context.Courses.Any(c => c.CourseName == course.CourseName))
            {
                return BadRequest("Course already exists.");
            }

            _context.Courses.Add(course);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCourses), new { id = course.Id }, course);
        }
    }
}
