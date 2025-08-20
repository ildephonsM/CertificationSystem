using System.ComponentModel.DataAnnotations;

namespace CertificationApp.Shared.Models
{
    public class Courses
    {
        public Guid Id { get; set; }
        public string CourseName { get; set; }
    }
}
