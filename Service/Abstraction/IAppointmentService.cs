using HospitalSystem.Models;

namespace HospitalSystem.Service.Abstraction
{
    public interface IAppointmentService
    {
        public List<Room> GetAllRooms();
        public RoomAvailability? BookRoom(Room room, DateTime date, TimeSpan time, string patientId, int DoctorId);
        public List<Bill> GetMyBills(string PatientId);

        public List<Appointment> GetMyAppointments(string patientId);

        public List<Appointment> GetDoctorMyAppointments(int Id);


    }
}
