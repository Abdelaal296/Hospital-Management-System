using AutoMapper;
using HospitalSystem.Helper;
using HospitalSystem.Models;
using HospitalSystem.ModelVM.Doctor;
using HospitalSystem.Repo.Abstraction;
using HospitalSystem.Service.Abstraction;

namespace HospitalSystem.Service.Impelementation
{
    public class DocServices : IDocServices
    {
        private readonly IDoctorRepo dectorRepo;
        private readonly IMapper mapper;

        public DocServices(IDoctorRepo dectorRepo, IMapper mapper)
        {
            this.dectorRepo = dectorRepo;
            this.mapper = mapper;
        }

        bool IDocServices.Create(CreateDocVM createDocVM)
        {
            createDocVM.Image = UploadImage.UploadFile("DProfile", createDocVM.ImageName);
            var data = mapper.Map<Doctor>(createDocVM);
            return dectorRepo.Create(data);
        }

        bool IDocServices.Delete(DeleteDocVM deleteDocVM)
        {
            var data = mapper.Map<Doctor>(deleteDocVM);
            return dectorRepo.Delete(data);
        }

        bool IDocServices.Edit(EditDocVM editDocVM)
        {
            var data = mapper.Map<Doctor>(editDocVM);
            return dectorRepo.Edit(data);
        }

        List<GetAllDoctorsVM> IDocServices.GetAllDoctors()
        {
            var data = dectorRepo.GetDoctors();
            var newData = mapper.Map<List<GetAllDoctorsVM>>(data);
            return newData;

        }

        GetByIdDocVM IDocServices.GetByIdDocVM(int id)
        {
            var data = dectorRepo.GetById(id);
            var newdata = mapper.Map<GetByIdDocVM>(data);
            return newdata;
        }

        Doctor IDocServices.GetDoctorByEmail(string email)
        {
            return dectorRepo.GetDoctorByEmail(email);
        }
    }
}
