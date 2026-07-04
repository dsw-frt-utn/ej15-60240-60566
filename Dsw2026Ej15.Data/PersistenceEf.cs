using System;
using System.Collections.Generic;
using System.Text;

using Dsw2026Ej15.Data.Abstractions;
using Dsw2026Ej15.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Dsw2026Ej15.Data
{
    public class PersistenceEf : IPersistence
    {
        private readonly Dsw2026Ej15DbContext _context;

        public PersistenceEf(Dsw2026Ej15DbContext context)
        {
            _context = context;
        }

        public async Task<List<Doctor>> GetDoctors()
        {
            return await _context.Doctors
                .Where(d => d.IsActive)
                .Include(d => d.Speciality)
                .ToListAsync();
        }

        public async Task<Doctor?> GetDoctorById(Guid id)
        {
            return await _context.Doctors
                .Include(d => d.Speciality)
                .FirstOrDefaultAsync(d => d.Id == id && d.IsActive);
        }

        public async Task<Speciality?> GetSpecialityById(Guid specialityId)
        {
            return await _context.Specialities
                .FirstOrDefaultAsync(s => s.Id == specialityId);
        }

        public async Task SaveDoctor(Doctor doctor)
        {
            _context.Add(doctor);
            await _context.SaveChangesAsync();
        }

        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }

    }
}
