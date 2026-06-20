using Dsw2026Ej15.Api.Models;
using Dsw2026Ej15.Data.Abstractions;
using Dsw2026Ej15.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Dsw2026Ej15.Domain.Exceptions;

namespace Dsw2026Ej15.Api.Controllers;

public class DoctorsController : AppController
{
    private readonly IPersistence _persistence;
    public DoctorsController(IPersistence persistence)
    {
        _persistence = persistence;
    }

    //Endpoint 1
    [HttpPost("doctors")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateDoctor(DoctorModel.Request request)
    {
        if (string.IsNullOrWhiteSpace(request.Name))
        {
            throw new ValidationException("Name es requerido");
        }

        if (string.IsNullOrWhiteSpace(request.LicenseNumber))
        {
            throw new ValidationException("LicenseNumber es requerido");
        }

        var speciality = _persistence.GetSpecialityById(request.SpecialityId);

        if (speciality is null)
        {
            throw new ValidationException("SpecialityId debe existir");
        }

        var doctor = new Doctor(
            request.Name,
            request.LicenseNumber,
            speciality);

        _persistence.SaveDoctor(doctor);

        return Created("", doctor);
    }
    //Endpoint 3
    [HttpGet("doctors/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetDoctor(Guid id)
    {
        var doctor = _persistence.GetDoctorById(id);

        if (doctor is null || !doctor.IsActive)
        {
            return NotFound();
        }

        return Ok(new
        {
            doctor.Name,
            doctor.LicenseNumber,
            SpecialityName = doctor.Speciality.Name
        });
    }

    //Endpoint 4
    [HttpDelete("doctors/{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteDoctor(Guid id)
    {
        var doctor = _persistence.GetDoctorById(id);

        if (doctor is null)
        {
            return NotFound();
        }

        doctor.Deactivate();

        return NoContent();
    }

    [HttpGet("doctors")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult GetDoctors()
    {
        var doctors = _persistence.GetDoctors();

        return Ok(doctors);
    }
}
