using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CertificationApp.Shared.Models
{
    public class Venues
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Trainee Name is required")]
        public string TraineeName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Course Date is required")]
        public DateTime CourseDate { get; set; }

        [Required(ErrorMessage = "CourseId is required")]
        public Guid CourseId { get; set; }

       
        [ForeignKey("CourseId")]
        public Courses? Course { get; set; }
    }
}
