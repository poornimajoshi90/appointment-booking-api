using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppointmentBookingAPI.Models
{
    public class Prescription
    {
        public int Id { get; set; }

        public int ConsultationId { get; set; }

        [ForeignKey("ConsultationId")]
        public Consultation Consultation { get; set; }

        [Required]
        public string Medicines { get; set; }

        [Required]
        public string Dosage { get; set; }

        public string? Instructions { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
