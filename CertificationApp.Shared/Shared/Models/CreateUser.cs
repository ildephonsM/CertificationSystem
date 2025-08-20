using System.ComponentModel.DataAnnotations;

namespace CertificationApp.Shared.Models
{
    public class CreateUser
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Surname is required")]
        public string Surname { get; set; } = string.Empty;

        [Required(ErrorMessage = "ID Number is required")]
        public string IdNumber { get; set; } = string.Empty;

        [Required(ErrorMessage = "Course Name is required")]
        public string CourseName { get; set; } = string.Empty;

        public DateTime DateCompleted { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
