using HospitalSystem.Models;

namespace HospitalSystem.Repo.Abstraction
{
    public interface IAMedicalReportRepo
    {
        Task AddReportAsync(MedicalReport report);
        Task<List<MedicalReport>> GetAllReportsAsync();
        Task<MedicalReport> GetReportByIdAsync(int reportId);
        Task UpdateReportAsync(MedicalReport report);
        bool Delete(int id);
    }
}
