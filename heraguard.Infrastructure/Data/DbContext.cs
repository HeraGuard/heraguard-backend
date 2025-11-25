using heraguard.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace heraguard.Infrastructure.Data;

public class HeraGuardDbContext : DbContext
{
    public HeraGuardDbContext(DbContextOptions<HeraGuardDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users => Set<User>();
    public DbSet<Role> Roles => Set<Role>();
    public DbSet<ElderProfile> Elders => Set<ElderProfile>();
    public DbSet<CaregiverProfile> Caregivers => Set<CaregiverProfile>();
    public DbSet<DoctorProfile> Doctors => Set<DoctorProfile>();

    public DbSet<Medication> Medications => Set<Medication>();
    public DbSet<Activity> Activities => Set<Activity>();
    public DbSet<MedicalAppointment> MedicalAppointments => Set<MedicalAppointment>();
    public DbSet<Prescription> Prescriptions => Set<Prescription>();
    public DbSet<Relationship> Relationships => Set<Relationship>();
    
    public DbSet<UserDeviceToken> UserDeviceTokens => Set<UserDeviceToken>();
    public DbSet<MedicationSchedule> MedicationSchedules => Set<MedicationSchedule>();
    public DbSet<MedicationIntakeLog> MedicationIntakeLogs => Set<MedicationIntakeLog>();
    public DbSet<CaregiverAlert> CaregiverAlerts => Set<CaregiverAlert>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //PK
        modelBuilder.Entity<ElderProfile>()
            .HasKey(p => p.UserId);

        modelBuilder.Entity<CaregiverProfile>()
            .HasKey(p => p.UserId);

        modelBuilder.Entity<DoctorProfile>()
            .HasKey(p => p.UserId);

        modelBuilder.Entity<Medication>()
            .HasKey(m => m.MedicationId);

        modelBuilder.Entity<Activity>()
            .HasKey(a => a.ActivityId);

        modelBuilder.Entity<MedicalAppointment>()
            .HasKey(m => m.MedicalAppointmentId);

        //Relaciones
        modelBuilder.Entity<User>()
            .HasOne(u => u.Role)
            .WithMany()
            .HasForeignKey(u => u.RoleId);

        modelBuilder.Entity<User>()
            .HasOne(u => u.AdultoMayorProfile)
            .WithOne(p => p.User)
            .HasForeignKey<ElderProfile>(p => p.UserId);

        modelBuilder.Entity<User>()
            .HasOne(u => u.FamiliarProfile)
            .WithOne(p => p.User)
            .HasForeignKey<CaregiverProfile>(p => p.UserId);

        modelBuilder.Entity<User>()
            .HasOne(u => u.DoctorProfile)
            .WithOne(p => p.User)
            .HasForeignKey<DoctorProfile>(p => p.UserId);

        modelBuilder.Entity<Medication>()
            .HasOne(m => m.DoctorProfile)
            .WithMany()
            .HasForeignKey(m => m.DoctorId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Medication>()
            .HasOne(m => m.CaregiverProfile)
            .WithMany()
            .HasForeignKey(m => m.CaregiverId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Medication>()
            .HasOne(m => m.ElderProfile)
            .WithMany()
            .HasForeignKey(m => m.ElderId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Activity>()
            .HasOne(a => a.DoctorProfile)
            .WithMany()
            .HasForeignKey(a => a.DoctorId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Activity>()
            .HasOne(a => a.CaregiverProfile)
            .WithMany()
            .HasForeignKey(a => a.CaregiverId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Activity>()
            .HasOne(a => a.ElderProfile)
            .WithMany()
            .HasForeignKey(a => a.ElderId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<MedicalAppointment>()
            .HasOne(m => m.DoctorProfile)
            .WithMany()
            .HasForeignKey(m => m.DoctorId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<MedicalAppointment>()
            .HasOne(m => m.CaregiverProfile)
            .WithMany()
            .HasForeignKey(m => m.CaregiverId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<MedicalAppointment>()
            .HasOne(m => m.ElderProfile)
            .WithMany()
            .HasForeignKey(m => m.ElderId)
            .OnDelete(DeleteBehavior.Restrict);
        
        // Prescription
        modelBuilder.Entity<Prescription>()
            .HasKey(r => r.Id);

        modelBuilder.Entity<Prescription>()
            .HasMany(r => r.Medications)
            .WithOne(m => m.Prescription)
            .HasForeignKey(m => m.PrescriptionId)
            .OnDelete(DeleteBehavior.Cascade);
        
        modelBuilder.Entity<Prescription>()
            .HasOne(p => p.Elder)
            .WithMany()
            .HasForeignKey(p => p.ElderId);

        modelBuilder.Entity<Prescription>()
            .HasOne(p => p.Doctor)
            .WithMany()
            .HasForeignKey(p => p.DoctorId);

        // Configuración de Relationship
        modelBuilder.Entity<Relationship>()
            .HasKey(r => r.RelationshipId);

        modelBuilder.Entity<Relationship>()
            .HasOne(r => r.Elder)
            .WithMany()
            .HasForeignKey(r => r.ElderId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Relationship>()
            .HasOne(r => r.RelatedUser)
            .WithMany()
            .HasForeignKey(r => r.RelatedUserId)
            .OnDelete(DeleteBehavior.Restrict);
        
        // UserDeviceToken
        modelBuilder.Entity<UserDeviceToken>()
            .HasKey(t => t.Id);

        modelBuilder.Entity<UserDeviceToken>()
            .HasOne(t => t.User)
            .WithMany()
            .HasForeignKey(t => t.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<UserDeviceToken>()
            .HasIndex(t => t.UserId);

        // MedicationSchedule
        modelBuilder.Entity<MedicationSchedule>()
            .HasKey(s => s.Id);

        modelBuilder.Entity<MedicationSchedule>()
            .HasOne(s => s.Medication)
            .WithMany()
            .HasForeignKey(s => s.MedicationId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<MedicationSchedule>()
            .HasOne(s => s.Elder)
            .WithMany()
            .HasForeignKey(s => s.ElderId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<MedicationSchedule>()
            .HasIndex(s => new { s.MedicationId, s.Status });

        // MedicationIntakeLog
        modelBuilder.Entity<MedicationIntakeLog>()
            .HasKey(l => l.Id);

        modelBuilder.Entity<MedicationIntakeLog>()
            .HasOne(l => l.Schedule)
            .WithMany()
            .HasForeignKey(l => l.MedicationScheduleId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<MedicationIntakeLog>()
            .HasOne(l => l.Medication)
            .WithMany()
            .HasForeignKey(l => l.MedicationId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<MedicationIntakeLog>()
            .HasOne(l => l.Elder)
            .WithMany()
            .HasForeignKey(l => l.ElderId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<MedicationIntakeLog>()
            .HasOne(l => l.ConfirmedBy)
            .WithMany()
            .HasForeignKey(l => l.ConfirmedByUserId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<MedicationIntakeLog>()
            .HasIndex(l => l.ElderId);

        // CaregiverAlert
        modelBuilder.Entity<CaregiverAlert>()
            .HasKey(a => a.Id);

        modelBuilder.Entity<CaregiverAlert>()
            .HasOne(a => a.Caregiver)
            .WithMany()
            .HasForeignKey(a => a.CaregiverId)
            .HasPrincipalKey(c => c.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<CaregiverAlert>()
            .HasOne(a => a.Elder)
            .WithMany()
            .HasForeignKey(a => a.ElderId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<CaregiverAlert>()
            .HasOne(a => a.Medication)
            .WithMany()
            .HasForeignKey(a => a.MedicationId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<CaregiverAlert>()
            .HasIndex(a => new { a.CaregiverId, a.IsRead });

    }
}