using Dsw2026Ej15.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dsw2026Ej15.Data.Abstractions
{
    public interface IPersistence
    {
        Speciality? GetSpecialityById(Guid specialityId);
        void SaveDcoctor(Doctor doctor);
        void SaveDoctor(Doctor doctor);
    }
}
