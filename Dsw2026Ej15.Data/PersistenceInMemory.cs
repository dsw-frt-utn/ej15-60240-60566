using System;
using System.Collections.Generic;
using System.Text.Json;
using Dsw2026Ej15.Domain.Entities;
using Dsw2026Ej15.Data.Abstractions;
using Dsw2026Ej15.Data.Dto;

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

        public Task<Speciality?> GetSpecialityById(Guid specialityId)
        {
            return Task.FromResult(_specialities.SingleOrDefault(s => s.Id == specialityId));
        }

        public Task SaveDoctor(Doctor doctor)
        {
            _doctors.Add(doctor);
            return Task.CompletedTask;
        }

        public Task<Doctor?> GetDoctorById(Guid id)
        {
            return Task.FromResult(_doctors.SingleOrDefault(d => d.Id == id));
        }

        public Task<List<Doctor>> GetDoctors()
        {
            return Task.FromResult(_doctors);
        }

        public Task SaveChanges()
        {
            return Task.CompletedTask;
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

                var specialities = JsonSerializer.Deserialize<List<SpecialityDto>>(
                    json,
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });

                if (specialities is null)
                {
                    return;
                }

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

    }
}
