using Dsw2026Ej15.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dsw2026Ej15.Data.Abstractions
{
    public interface IPersistence
    {
        Speciality? GetSpecialityById(Guid specialityId);

        void SaveDoctor(Doctor doctor);

        Doctor? GetDoctorById(Guid id);

        List<Doctor> GetDoctors();
    }
}