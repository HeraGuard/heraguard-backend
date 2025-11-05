using heraguard.Application.MedicalAppointments.Interfaces;
using heraguard.Domain.Entities;
using heraguard.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace heraguard.Infrastructure.Repositories;

public class MedicalAppointmentRepository : IMedicalAppointmentRepository
{
    private readonly HeraGuardDbContext _context;

    public MedicalAppointmentRepository(HeraGuardDbContext context)
    {
        _context = context;
    }

    public async Task<MedicalAppointment> AddMedicalAppointmentAsync(MedicalAppointment appointment)
    {
        await _context.MedicalAppointments.AddAsync(appointment);
        await _context.SaveChangesAsync();
        return appointment;
    }

    public async Task<MedicalAppointment> UpdateMedicalAppointmentAsync(MedicalAppointment appointment)
    {
        _context.MedicalAppointments.Update(appointment);
        await _context.SaveChangesAsync();
        return appointment;
    }

    public async Task<bool> DeleteMedicalAppointmentAsync(MedicalAppointment appointment)
    {
        _context.MedicalAppointments.Remove(appointment);
        var affected = await _context.SaveChangesAsync();
        return affected > 0;
    }

    public async Task<MedicalAppointment?> GetMedicalAppointmentByIdAsync(Guid medicalAppointmentId)
    {
        return await _context.MedicalAppointments
            .FirstOrDefaultAsync(m => m.MedicalAppointmentId == medicalAppointmentId);
    }

    public async Task<MedicalAppointment?> GetMedicalAppointmentByIdWithRelationAsync(Guid medicalAppointmentId)
    {
        return await _context.MedicalAppointments
            .Include(m => m.DoctorProfile)
                .ThenInclude(d => d.User)
            .Include(m => m.CaregiverProfile)
                .ThenInclude(c => c.User)
            .Include(m => m.ElderProfile)
                .ThenInclude(e => e.User)
            .FirstOrDefaultAsync(m => m.MedicalAppointmentId == medicalAppointmentId);
    }

    public async Task<List<MedicalAppointment>> GetAllMedicalAppointmentByUserIdAsync(Guid userId)
    {
        return await _context.MedicalAppointments
            .Where(m => m.DoctorId == userId || m.CaregiverId == userId || m.ElderId == userId)
            .Include(m => m.DoctorProfile)
                .ThenInclude(d => d.User)
            .Include(m => m.CaregiverProfile)
                .ThenInclude(c => c.User)
            .Include(m => m.ElderProfile)
                .ThenInclude(e => e.User)
            .ToListAsync();
    }
}
