using System.ComponentModel.DataAnnotations;

namespace AppointmentBookingAPI.DTO
{
    public class CreateAppointmentDTO
    {
        [Required]
        public DateTime AppointmentDate { get; set; }

        [Required]
        public string Reason { get; set; }
    }
}
