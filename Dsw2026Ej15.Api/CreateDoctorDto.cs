namespace Dsw2026Ej15.Api
{
    public record CreateDoctorDto(
        string Name,
        string LicenseNumber,
        Guid SpecialityId
        );

}
