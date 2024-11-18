using HospitalSystem.Models;

namespace HospitalSystem.Repo.Abstraction
{
    public interface IDoctorRepo
    {
        List<Doctor> GetDoctors();
        bool Edit(Doctor doc);
        bool Create(Doctor doc);
        bool Delete(Doctor doc);
        Doctor GetById(int id);
        Doctor GetDoctorByEmail(string email);
    }
}
