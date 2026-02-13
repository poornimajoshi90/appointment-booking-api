using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AppointmentBookingAPI.Models;
using AppointmentBookingAPI.Services;

namespace AppointmentBookingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PrescriptionsController : ControllerBase
    {
        private readonly IPrescriptionService _prescriptionService;

        public PrescriptionsController(IPrescriptionService prescriptionService)
        {
            _prescriptionService = prescriptionService;
        }

        [Authorize(Roles = "Doctor")]
        [HttpPost]
        public async Task<IActionResult> CreatePrescription(Prescription prescription)
        {
            await _prescriptionService.CreatePrescriptionAsync(prescription);

            return Ok(new
            {
                success = true,
                message = "Prescription Created Successfully"
            });
        }
    }
}
