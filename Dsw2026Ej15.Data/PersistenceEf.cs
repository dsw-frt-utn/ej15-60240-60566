using Dsw2026Ej15.Data.Abstractions;
using Dsw2026Ej15.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dsw2026Ej15.Data
{
    public class PersistenceEf : IPersistence
    {
        private readonly Dsw2026Ej15DbContext _context;
        public PersistenceEf(Dsw2026Ej15DbContext context)
        {
            _context = context;
        }

        public Doctor? GetDoctorById(Guid id)
        {
            return _context.Doctors.Include(d => d.Speciality).SingleOrDefault(d => d.Id == id);
        }

        public List<Doctor> GetDoctors()
        {
            return _context.Doctors.Include(d => d.Speciality).Where(d => d.IsActive).ToList();
        }

        public Speciality? GetSpecialityById(Guid specialityId)
        {
            return _context.Specialities.SingleOrDefault(s => s.Id == specialityId);
        }

        public void SaveDoctor(Doctor doctor)
        {
            _context.Doctors.Add(doctor);
            _context.SaveChanges();
        }
    }
}
