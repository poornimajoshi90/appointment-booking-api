using System.Text.Json;
using AppointmentBookingAPI.Data;
using AppointmentBookingAPI.DTO;
using AppointmentBookingAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration.UserSecrets;
using AppointmentBookingAPI.Helpers;


namespace AppointmentBookingAPI.Services
{
    public interface IAppointmentService
    {
        Task<object> CreateAppointmentAsync(CreateAppointmentDTO dto, int userId, string idempotencyKey);
        Task<IEnumerable<Appointment>> GetUserAppointmentsAsync(int userId);
        Task<IEnumerable<Appointment>> GetAllAppointmentsAsync();
        Task ApproveAppointmentAsync(int appointmentId, int doctorId);
        Task RejectAppointmentAsync(int appointmentId);
    }

    public class AppointmentService : IAppointmentService
    {
        private readonly AppDbContext _context;
        private readonly ILogger<AppointmentService> _logger;

        public AppointmentService(AppDbContext context, ILogger<AppointmentService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<object> CreateAppointmentAsync(CreateAppointmentDTO dto,int userId,
     string idempotencyKey)
        {
            await using var transaction =
                await _context.Database.BeginTransactionAsync();

            try
            {
                // Generate request hash
                var requestHash = HashHelper.GenerateHash(dto);

                //  Check Idempotency
                var existingKey = await _context.IdempotencyKeys
                    .FirstOrDefaultAsync(x => x.Key == idempotencyKey);

                if (existingKey != null)
                {
                    if (existingKey.RequestHash != requestHash)
                        throw new ArgumentException(
                            "Idempotency key reused with different payload");

                    return JsonSerializer.Deserialize<object>(
                        existingKey.Response)!;
                }

                // Prevent double booking
                var isSlotBooked = await _context.Appointment
                    .AnyAsync(a =>
                        a.AppointmentDate == dto.AppointmentDate &&
                        a.Status != Enums.AppointmentStatus.Rejected);

                if (isSlotBooked)
                {
                    throw new InvalidOperationException(
                        "This time slot is already booked.");
                }

                // Create Appointment
                var appointment = new Appointment
                {
                    AppointmentDate = dto.AppointmentDate,
                    Reason = dto.Reason,
                    UserId = userId,
                    Status = Enums.AppointmentStatus.Pending
                };

                await _context.Appointment.AddAsync(appointment);
                await _context.SaveChangesAsync();

                var response = new
                {
                    success = true,
                    appointmentId = appointment.Id
                };

                // Store idempotency record
                _context.IdempotencyKeys.Add(new IdempotencyKey
                {
                    Key = idempotencyKey,
                    RequestHash = requestHash,
                    Response = JsonSerializer.Serialize(response)
                });

                await _context.SaveChangesAsync();

                await transaction.CommitAsync();

                _logger.LogInformation(
                    "Appointment created successfully for UserId {UserId}",
                    userId);

                return response;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();

                _logger.LogError(ex,
                    "Error while creating appointment for UserId {UserId}",
                    userId);

                throw;
            }
        }



        public async Task<IEnumerable<Appointment>> GetUserAppointmentsAsync(int userId)
        {
            try
            {
                return await _context.Appointment
                    .Where(a => a.UserId == userId)
                    .OrderByDescending(a => a.AppointmentDate)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching user appointments");
                throw;
            }
        }

        public async Task<IEnumerable<Appointment>> GetAllAppointmentsAsync()
        {
            try
            {
                return await _context.Appointment
                    .Include(a => a.User)
                    .OrderByDescending(a => a.AppointmentDate)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching all appointments");
                throw;
            }
        }

        public async Task ApproveAppointmentAsync(int appointmentId, int doctorId)
        {
            try
            {
                var appointment = await _context.Appointment
                    .FirstOrDefaultAsync(a => a.Id == appointmentId);

                if (appointment == null)
                    throw new KeyNotFoundException("Appointment not found");

                if (appointment.Status != Enums.AppointmentStatus.Pending)
                    throw new ArgumentException("Appointment already processed");

                var doctor = await _context.Users
                    .FirstOrDefaultAsync(u => u.Id == doctorId && u.Role == "Doctor");

                if (doctor == null)
                    throw new ArgumentException("Invalid doctor");

                appointment.Status = Enums.AppointmentStatus.Approved;
                appointment.DoctorId = doctorId;

                var consultation = new Consultation
                {
                    AppointmentId = appointment.Id,
                    Status = Enums.ConsultationStatus.Scheduled
                };

                await _context.Consultations.AddAsync(consultation);
                await _context.SaveChangesAsync();

                _logger.LogInformation(
                    "Appointment {AppointmentId} approved by Doctor {DoctorId}",
                    appointmentId, doctorId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error approving appointment {AppointmentId}", appointmentId);
                throw;
            }
        }

        public async Task RejectAppointmentAsync(int appointmentId)
        {
            try
            {
                var appointment = await _context.Appointment
                    .FirstOrDefaultAsync(a => a.Id == appointmentId);

                if (appointment == null)
                    throw new KeyNotFoundException("Appointment not found");

                if (appointment.Status != Enums.AppointmentStatus.Pending)
                    throw new ArgumentException("Only pending appointments can be rejected");

                appointment.Status = Enums.AppointmentStatus.Rejected;

                await _context.SaveChangesAsync();

                _logger.LogInformation("Appointment {AppointmentId} rejected", appointmentId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error rejecting appointment {AppointmentId}", appointmentId);
                throw;
            }
        }
    }
}
