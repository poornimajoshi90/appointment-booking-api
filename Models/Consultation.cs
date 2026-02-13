using System.ComponentModel.DataAnnotations.Schema;
using AppointmentBookingAPI.Enums;

namespace AppointmentBookingAPI.Models
{
    public class Consultation
    {
        public int Id { get; set; }

        public int AppointmentId { get; set; }

        [ForeignKey("AppointmentId")]
        public Appointment Appointment { get; set; }

        public ConsultationStatus Status { get; set; } = ConsultationStatus.Scheduled;

        // Scheduled → InProgress → Completed

        public string? Notes { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
