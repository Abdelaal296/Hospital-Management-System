using HospitalSystem.Models;
using HospitalSystem.ModelVM.Doctor;
using HospitalSystem.Repo.Abstraction;

namespace HospitalSystem.Service.Abstraction
{
    public interface IDocServices
    {
        List<GetAllDoctorsVM> GetAllDoctors();
        bool Create(CreateDocVM createDocVM);
        bool Delete(DeleteDocVM deleteDocVM);
        bool Edit(EditDocVM editDocVM);
        GetByIdDocVM GetByIdDocVM(int id);

        Doctor GetDoctorByEmail(string email);

    }
}
