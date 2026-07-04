using Dsw2026Ej15.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dsw2026Ej15.Data.Abstractions
{
    public interface IPersistence
    {
        Task<Speciality?> GetSpecialityById(Guid specialityId);

        Task SaveDoctor(Doctor doctor);

        Task<Doctor?> GetDoctorById(Guid id);

        Task<List<Doctor>> GetDoctors();

        Task SaveChanges();
    }
}