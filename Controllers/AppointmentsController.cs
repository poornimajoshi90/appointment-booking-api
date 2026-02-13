using System.Security.Claims;
using AppointmentBookingAPI.Data;
using AppointmentBookingAPI.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using AppointmentBookingAPI.Services;

namespace AppointmentBookingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AppointmentsController : ControllerBase
    {
        private readonly IAppointmentService _appointmentService;

        public AppointmentsController(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }


        [HttpPost]
        public async Task<IActionResult> CreateAppointment(CreateAppointmentDTO dto)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var idempotencyKey =
                Request.Headers["Idempotency-Key"].FirstOrDefault();

            if (string.IsNullOrEmpty(idempotencyKey))
                return BadRequest("Idempotency-Key header required");

            var result = await _appointmentService
                .CreateAppointmentAsync(dto, userId, idempotencyKey);

            return Ok(result);
        }


        [Authorize(Roles = "User,Doctor")]
        [HttpGet("my-appointments")]
        public async Task<IActionResult> GetMyAppointments()
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var appointments = await _appointmentService.GetUserAppointmentsAsync(userId);

            return Ok(new
            {
                success = true,
                data = appointments
            });
        }

        
        [Authorize(Roles = "Doctor")]
        [HttpGet]
        public async Task<IActionResult> GetAllAppointments()
        {
            var appointments = await _appointmentService.GetAllAppointmentsAsync();

            return Ok(new
            {
                success = true,
                data = appointments
            });
        }

        [Authorize(Roles = "Doctor")]
        [HttpPut("{id}/approve")]
        public async Task<IActionResult> ApproveAppointment(int id, [FromQuery] int doctorId)
        {
            await _appointmentService.ApproveAppointmentAsync(id, doctorId);

            return Ok(new
            {
                success = true,
                message = "Appointment approved successfully"
            });
        }

        
        [Authorize(Roles = "Doctor")]
        [HttpPut("{id}/reject")]
        public async Task<IActionResult> RejectAppointment(int id)
        {
            await _appointmentService.RejectAppointmentAsync(id);

            return Ok(new
            {
                success = true,
                message = "Appointment rejected successfully"
            });
        }
    }
}
