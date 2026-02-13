using AppointmentBookingAPI.Data;
using AppointmentBookingAPI.DTO;
using AppointmentBookingAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace AppointmentBookingAPI.Services
{
    public interface IUserService
    {
        Task RegisterAsync(RegisterDTO dto);
        Task<string> LoginAsync(LoginDTO dto);
    }

    public class UserService : IUserService
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly ILogger<UserService> _logger;

        public UserService(AppDbContext context,
                           IConfiguration configuration,
                           ILogger<UserService> logger)
        {
            _context = context;
            _configuration = configuration;
            _logger = logger;
        }

        public async Task RegisterAsync(RegisterDTO dto)
        {
            try
            {
                var existingUser = await _context.Users
                    .FirstOrDefaultAsync(u => u.Email == dto.Email);

                if (existingUser != null)
                    throw new ArgumentException("Email already registered");

                var user = new User
                {
                    Name = dto.Name,
                    Email = dto.Email,
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                    Role = "User"
                };

                _context.Users.Add(user);
                await _context.SaveChangesAsync();

                _logger.LogInformation("User registered with Email {Email}", dto.Email);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error registering user {Email}", dto.Email);
                throw;
            }
        }

        public async Task<string> LoginAsync(LoginDTO dto)
        {
            try
            {
                var user = await _context.Users
                    .FirstOrDefaultAsync(u => u.Email == dto.Email);

                if (user == null)
                    throw new UnauthorizedAccessException("Invalid Email");

                bool isPasswordValid =
                    BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash);

                if (!isPasswordValid)
                    throw new UnauthorizedAccessException("Invalid Password");

                var claims = new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Role, user.Role)
                };

                var key = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(
                    issuer: _configuration["Jwt:Issuer"],
                    audience: _configuration["Jwt:Audience"],
                    claims: claims,
                    expires: DateTime.Now.AddHours(2),
                    signingCredentials: creds
                );

                var jwt = new JwtSecurityTokenHandler().WriteToken(token);

                _logger.LogInformation("User logged in {Email}", dto.Email);

                return jwt;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during login for {Email}", dto.Email);
                throw;
            }
        }
    }
}

