using AppointmentBookingAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace AppointmentBookingAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Appointment> Appointment { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Consultation> Consultations { get; set; }
        public DbSet<Prescription> Prescriptions { get; set; }
        public DbSet<IdempotencyKey> IdempotencyKeys { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Unique Idempotency Key
            modelBuilder.Entity<IdempotencyKey>()
                .HasIndex(x => x.Key)
                .IsUnique();

            // Unique Email
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            // One Prescription per Consultation
            modelBuilder.Entity<Prescription>()
                .HasIndex(p => p.ConsultationId)
                .IsUnique();

            // Prevent Double Booking (IMPORTANT)
            modelBuilder.Entity<Appointment>().HasIndex(a => new { a.DoctorId, a.AppointmentDate }).IsUnique();
        }
    }
}

