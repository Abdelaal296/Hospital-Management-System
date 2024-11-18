using HospitalSystem.Models;

namespace HospitalSystem.Repo.Abstraction
{
    public interface IMedicalReportRepo
    {
        MedicalReport GetByReportIdAndPatientId(string reportId, string patientNationalId);
        List<MedicalReport> GetReportsByPatientId(string patientId);
    }
}
