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
    public DbSet<AdultoMayorProfile> AdultosMayores => Set<AdultoMayorProfile>();
    public DbSet<FamiliarProfile> Familiares => Set<FamiliarProfile>();
    public DbSet<DoctorProfile> Doctores => Set<DoctorProfile>();
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //PK
        modelBuilder.Entity<AdultoMayorProfile>()
            .HasKey(p => p.UserId);
    
        modelBuilder.Entity<FamiliarProfile>()
            .HasKey(p => p.UserId);
    
        modelBuilder.Entity<DoctorProfile>()
            .HasKey(p => p.UserId);
        
        
        
        //Relaciones
        modelBuilder.Entity<User>()
            .HasOne(u => u.Role)
            .WithMany()
            .HasForeignKey(u => u.RoleId);

        modelBuilder.Entity<User>()
            .HasOne(u => u.AdultoMayorProfile)
            .WithOne(p => p.User)
            .HasForeignKey<AdultoMayorProfile>(p => p.UserId);

        modelBuilder.Entity<User>()
            .HasOne(u => u.FamiliarProfile)
            .WithOne(p => p.User)
            .HasForeignKey<FamiliarProfile>(p => p.UserId);

        modelBuilder.Entity<User>()
            .HasOne(u => u.DoctorProfile)
            .WithOne(p => p.User)
            .HasForeignKey<DoctorProfile>(p => p.UserId);
    }
}