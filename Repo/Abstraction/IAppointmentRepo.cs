using HospitalSystem.Models;

namespace HospitalSystem.Repo.Abstraction
{
    public interface IAppointmentRepo
    {
        public List<Room> GetAllRooms();
        public RoomAvailability? BookRoom(Room room, DateTime date, TimeSpan time, string PatientId, int DoctorId);

        public List<Bill> GetMyBills(string patientId);

        public List<Appointment> GetMyAppointments(string patientId);

        public List<Appointment> GetDoctorMyAppointments(int Id);
    }
}
