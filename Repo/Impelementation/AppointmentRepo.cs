using HospitalSystem.Data;
using HospitalSystem.Models;
using HospitalSystem.Repo.Abstraction;
using Microsoft.EntityFrameworkCore;

namespace HospitalSystem.Repo.Impelementation
{
    public class AppointmentRepo : IAppointmentRepo
    {

        private readonly AppDbContext db;
        public AppointmentRepo(AppDbContext db)
        {
            this.db = db;
        }

        public List<Room> GetAllRooms()
        {
            return db.Rooms.ToList();
        }
        public RoomAvailability? BookRoom(Room room, DateTime date, TimeSpan time, string PatientId, int DoctorId)
        {
            var existingAvailability = db.RoomAvailabilities
                .Where(ra => ra.RoomID == room.ID && ra.Date == date && ra.Time == time)
                .OrderByDescending(ra => ra.ID)
                .FirstOrDefault();


            if (existingAvailability == null)
            {
                var existingDoctor = db.Appointments.Where(a => a.DoctorID == DoctorId && a.Date == date && a.Time == time).FirstOrDefault();
                if (existingDoctor == null)
                {

                    var roomAvailability = new RoomAvailability
                    {
                        RoomID = room.ID,
                        Date = date,
                        Time = time,
                        AvailablePlaces = room.Capacity - 1, 
                        PatientID = PatientId
                    };
                    var bill = new Bill
                    {
                        Amount = 250,
                        PaymentDate = DateTime.Now,
                        PatientID = PatientId,
                        RoomAvailability = roomAvailability
                    };
                    var appiontment = new Appointment
                    {
                        PatientID = PatientId,
                        DoctorID = DoctorId,
                        RoomAvailability = roomAvailability,
                        Date = date,
                        Time = time,
                    };
                    db.RoomAvailabilities.Add(roomAvailability);
                    db.Bills.Add(bill);
                    db.Appointments.Add(appiontment);
                    db.SaveChanges();


                    return roomAvailability;
                }else
                {
                    return null;
                }
            }
            else if (existingAvailability.AvailablePlaces == 0)
            {
                return null;
            }
            else
            {
                var roomAvailability = new RoomAvailability
                {
                    RoomID = room.ID,
                    Date = date,
                    Time = time,
                    AvailablePlaces = existingAvailability.AvailablePlaces - 1
                };
                var bill = new Bill
                {
                    Amount = 250,
                    PaymentDate = DateTime.Now,
                    PatientID = PatientId,
                    RoomAvailability = roomAvailability
                };

                db.RoomAvailabilities.Add(roomAvailability);
                db.Bills.Add(bill);
                db.SaveChanges();
                return roomAvailability;
            }
        }

        public List<Bill> GetMyBills(string patientId)
        {
            return db.Bills.Where(b => b.PatientID == patientId).ToList();
        }

        public List<Appointment> GetMyAppointments(string patientId)
        {
            return db.Appointments.Include(a =>a.Doctor).Where(b => b.PatientID == patientId).ToList();
        }

        public List<Appointment> GetDoctorMyAppointments(int Id)
        {
            return db.Appointments.Include(a => a.Patient).Where(b => b.DoctorID == Id).ToList();
        }
    }
}
