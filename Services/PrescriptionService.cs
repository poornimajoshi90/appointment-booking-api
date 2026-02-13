using AppointmentBookingAPI.Data;
using AppointmentBookingAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace AppointmentBookingAPI.Services
{
    public interface IPrescriptionService
    {
        Task CreatePrescriptionAsync(Prescription prescription);
    }

    public class PrescriptionService : IPrescriptionService
    {
        private readonly AppDbContext _context;
        private readonly ILogger<PrescriptionService> _logger;

        public PrescriptionService(AppDbContext context, ILogger<PrescriptionService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task CreatePrescriptionAsync(Prescription prescription)
        {
            try
            {
                var consultation = await _context.Consultations
                    .FirstOrDefaultAsync(c => c.Id == prescription.ConsultationId);

                if (consultation == null)
                    throw new KeyNotFoundException("Consultation not found");

                if (consultation.Status != Enums.ConsultationStatus.Completed)
                    throw new ArgumentException("Consultation not completed yet");

                // Optional strong validation 
                var existingPrescription = await _context.Prescriptions
                    .FirstOrDefaultAsync(p => p.ConsultationId == prescription.ConsultationId);

                if (existingPrescription != null)
                    throw new ArgumentException("Prescription already exists for this consultation");

                _context.Prescriptions.Add(prescription);
                await _context.SaveChangesAsync();

                _logger.LogInformation(
                    "Prescription created for Consultation {ConsultationId}",
                    prescription.ConsultationId);
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Error creating prescription for Consultation {ConsultationId}",
                    prescription.ConsultationId);
                throw;
            }
        }
    }
}
