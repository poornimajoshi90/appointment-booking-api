using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AppointmentBookingAPI.Enums;

namespace AppointmentBookingAPI.Models
{
    public class Appointment
    {
        public int Id { get; set; }

        [Required]
        public DateTime AppointmentDate { get; set; }

        [Required]
        public string Reason { get; set; }

        public AppointmentStatus Status { get; set; } = AppointmentStatus.Pending;


        // Foreign Key
        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }
        public int? DoctorId { get; set; }

        [ForeignKey("DoctorId")]
        public User? Doctor { get; set; }
    }
}
