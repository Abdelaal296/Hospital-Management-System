using HospitalSystem.Models;
using HospitalSystem.ModelVM.Patient;
using HospitalSystem.Repo.Abstraction;
using HospitalSystem.Service.Abstraction;

namespace HospitalSystem.Service.Impelementation
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IAppointmentRepo AppointmentRepo;
        public AppointmentService(IAppointmentRepo AppointmentRepo)
        { this.AppointmentRepo = AppointmentRepo; }

        public List<Room> GetAllRooms()
        { return AppointmentRepo.GetAllRooms(); }
        public RoomAvailability? BookRoom(Room room, DateTime date, TimeSpan time, string PatientId, int DoctorId)
        {
            var result = AppointmentRepo.BookRoom(room, date, time, PatientId, DoctorId);
            return result;
        }
        public List<Bill> GetMyBills(string PatientId)
        {
            return AppointmentRepo.GetMyBills(PatientId);
        }
        public List<Appointment> GetMyAppointments(string patientId)
        {
            return AppointmentRepo.GetMyAppointments(patientId);
        }
        public List<Appointment> GetDoctorMyAppointments(int Id)
        {
            return AppointmentRepo.GetDoctorMyAppointments(Id);

        }

    }
}
