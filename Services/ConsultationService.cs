using AppointmentBookingAPI.Data;
using AppointmentBookingAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace AppointmentBookingAPI.Services
{
    public interface IConsultationService
    {
        Task StartConsultationAsync(int consultationId);
        Task CompleteConsultationAsync(int consultationId);
        Task<IEnumerable<Consultation>> GetDoctorConsultationsAsync(int doctorId);
    }

    public class ConsultationService : IConsultationService
    {
        private readonly AppDbContext _context;
        private readonly ILogger<ConsultationService> _logger;

        public ConsultationService(AppDbContext context, ILogger<ConsultationService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task StartConsultationAsync(int consultationId)
        {
            try
            {
                var consultation = await _context.Consultations
                    .FirstOrDefaultAsync(c => c.Id == consultationId);

                if (consultation == null)
                    throw new KeyNotFoundException("Consultation not found");

                if (consultation.Status != Enums.ConsultationStatus.Scheduled)
                    throw new ArgumentException("Consultation cannot be started");

                consultation.Status = Enums.ConsultationStatus.InProgress;

                await _context.SaveChangesAsync();

                _logger.LogInformation("Consultation {ConsultationId} started", consultationId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error starting consultation {ConsultationId}", consultationId);
                throw;
            }
        }

        public async Task CompleteConsultationAsync(int consultationId)
        {
            try
            {
                var consultation = await _context.Consultations
                    .FirstOrDefaultAsync(c => c.Id == consultationId);

                if (consultation == null)
                    throw new KeyNotFoundException("Consultation not found");

                if (consultation.Status != Enums.ConsultationStatus.InProgress)
                    throw new ArgumentException("Consultation must be in progress to complete");

                consultation.Status = Enums.ConsultationStatus.Completed;

                await _context.SaveChangesAsync();

                _logger.LogInformation("Consultation {ConsultationId} completed", consultationId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error completing consultation {ConsultationId}", consultationId);
                throw;
            }
        }

        public async Task<IEnumerable<Consultation>> GetDoctorConsultationsAsync(int doctorId)
        {
            try
            {
                return await _context.Consultations
                    .Include(c => c.Appointment)
                    .Where(c => c.Appointment.DoctorId == doctorId)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching consultations for doctor {DoctorId}", doctorId);
                throw;
            }
        }
    }
}

