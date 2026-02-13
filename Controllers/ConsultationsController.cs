using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using AppointmentBookingAPI.Services;

namespace AppointmentBookingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ConsultationsController : ControllerBase
    {
        private readonly IConsultationService _consultationService;

        public ConsultationsController(IConsultationService consultationService)
        {
            _consultationService = consultationService;
        }

       
        [Authorize(Roles = "Doctor")]
        [HttpPut("{id}/start")]
        public async Task<IActionResult> StartConsultation(int id)
        {
            await _consultationService.StartConsultationAsync(id);

            return Ok(new
            {
                success = true,
                message = "Consultation started successfully"
            });
        }

        
        [Authorize(Roles = "Doctor")]
        [HttpPut("{id}/complete")]
        public async Task<IActionResult> CompleteConsultation(int id)
        {
            await _consultationService.CompleteConsultationAsync(id);

            return Ok(new
            {
                success = true,
                message = "Consultation completed successfully"
            });
        }

       
        [Authorize(Roles = "Doctor")]
        [HttpGet("my-consultations")]
        public async Task<IActionResult> GetMyConsultations()
        {
            var doctorId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var consultations = await _consultationService.GetDoctorConsultationsAsync(doctorId);

            return Ok(new
            {
                success = true,
                data = consultations
            });
        }
    }
}

