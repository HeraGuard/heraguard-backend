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

    }
}