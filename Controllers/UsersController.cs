using AppointmentBookingAPI.DTO;
using AppointmentBookingAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AppointmentBookingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDTO dto)
        {
            await _userService.RegisterAsync(dto);

            return Ok(new
            {
                success = true,
                message = "User Registered Successfully"
            });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDTO dto)
        {
            var token = await _userService.LoginAsync(dto);

            return Ok(new
            {
                success = true,
                token = token
            });
        }

        [Authorize]
        [HttpGet("profile")]
        public IActionResult GetProfile()
        {
            return Ok("You are authenticated!");
        }

        [Authorize(Roles = "Doctor")]
        [HttpGet("doctor-dashboard")]
        public IActionResult DoctorDashboard()
        {
            return Ok("Welcome Doctor!");
        }
    }
}
