using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CertificationApp.Shared.Models
{
    public class Attendees
    {
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public string Surname { get; set; } = string.Empty;
        [Required]
        public string IdNumber { get; set; } = string.Empty;
        public DateTime DateCreated { get; set; }

        [Required]
        public Guid VenueId { get; set; }

        [ForeignKey("VenueId")]
        public Venues? Venues { get; set; }

    }
}
