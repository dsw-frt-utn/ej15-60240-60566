using System;
using System.Collections.Generic;
using System.Text.Json;
using Dsw2026Ej15.Domain.Entities;
using Dsw2026Ej15.Data.Abstractions;

namespace Dsw2026Ej15.Data
{
    public class PersistenceInMemory : IPersistence
    {
        private List<Speciality> _specialities = [];
        private List<Doctor> _doctors = [];

        public PersistenceInMemory()
        {
            LoadSpecialities();
        }

        public Speciality? GetSpeciality(Guid id)
        {
            return _specialities.SingleOrDefault(e => e.Id == id);
        }

        public Speciality? GetSpecialityById(Guid specialityId)
        {
            return _specialities.SingleOrDefault(s => s.Id == specialityId);
        }

        public void SaveDoctor(Doctor doctor)
        {
            _doctors.Add(doctor);
        }

        public Doctor? GetDoctorById(Guid id)
        {
            return _doctors.SingleOrDefault(d => d.Id == id);
        }

        public List<Doctor> GetDoctors()
        {
            return _doctors;
        }

        private void LoadSpecialities()
        {
            try
            {
                string jsonPath = Path.Combine(
                    AppDomain.CurrentDomain.BaseDirectory,
                    "Sources",
                    "specialities.json");

                var json = File.ReadAllText(jsonPath);

                var specialities = JsonSerializer.Deserialize<List<Speciality>>(
                    json,
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });

                _specialities =
                [
                    .. specialities.Select(s =>
                        new Speciality(s.Name, s.Description, s.Id))
                ];
            }
            catch (Exception)
            {
            }
        }

        public void AddDoctor(Doctor doctor)
        {
            _doctors.Add(doctor);
        }
    }
}
