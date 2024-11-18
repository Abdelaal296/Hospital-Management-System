using HospitalSystem.Models;
using System.Security.Claims;

namespace HospitalSystem.Service.Abstraction
{
    public interface IMedicalReportService
    {
        MedicalReport GetReport(string reportId, string patientNationalId);
        List<MedicalReport> GetPatientReports(ClaimsPrincipal user);
    }
}
