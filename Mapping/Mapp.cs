using AutoMapper;
using HospitalSystem.Models;
using HospitalSystem.ModelVM.Account;
using HospitalSystem.ModelVM.Doctor;
using HospitalSystem.ModelVM.MedicalReport;
using HospitalSystem.ModelVM.Patient;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace HospitalSystem.Mapping
{
    public class Mapp : Profile
    {
        public Mapp()
        {
            CreateMap<Patient, EditePatientVM>().ReverseMap();
            CreateMap<Patient, ChangePasswordVM>().ReverseMap();
            CreateMap<Patient, PatientProfileVM>().ReverseMap();
            CreateMap<Patient, PatientVM>().ReverseMap();
            CreateMap<Patient, LoginVM>().ReverseMap();
            CreateMap<RegistrationVM, Patient>()
            .ForMember(dest => dest.DOB, opt => opt.MapFrom(src => src.BirthDate));
            CreateMap<ConfirmEmailLoginVM, Patient>()
           .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.EmailAddress));
            CreateMap<Doctor, GetAllDoctorsVM>();
            CreateMap<Doctor, GetByIdDocVM>();
            CreateMap<CreateDocVM, Doctor>();
            CreateMap<DeleteDocVM, Doctor>();
            CreateMap<EditDocVM, Doctor>();
            CreateMap<MedicalReport, EditReportVM>().ReverseMap();
        }
    }
}
