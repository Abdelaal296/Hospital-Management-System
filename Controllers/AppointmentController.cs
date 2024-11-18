using HospitalSystem.Models;
using HospitalSystem.Service.Abstraction;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;

namespace HospitalSystem.Controllers
{
    public class AppointmentController : Controller
    {
        private readonly IAppointmentService AppointmentService;
        private readonly IDocServices _docServices;

        public AppointmentController(IAppointmentService AppointmentService, IDocServices docServices)
        {
            this.AppointmentService = AppointmentService;
            _docServices=docServices;
        }


        public IActionResult Book()
        {
            var doctors = _docServices.GetAllDoctors().Select(d => new { d.Id, DisplayName = $"{d.FullName} - {d.Specialization}" }); ;
            SelectList selectedList = new SelectList(doctors, "Id", "DisplayName");
            ViewBag.Doctors = selectedList;
            return View();
        }


        [HttpPost]
        public IActionResult Book(DateTime date, TimeSpan time,int DoctorId)
        {           
            var patientId = User.FindFirstValue(ClaimTypes.NameIdentifier); 

            if (string.IsNullOrEmpty(patientId))
            {
                ViewBag.ErrorMessage = "Unable to retrieve patient information. Please log in.";
                return View();
            }

            var rooms = AppointmentService.GetAllRooms();
            RoomAvailability? bookedRoom = null;

            foreach (var room in rooms)
            {
                var broom = AppointmentService.BookRoom(room, date, time, patientId, DoctorId);
                if (broom != null)
                {
                    bookedRoom = broom;
                    break;
                }
            }


            if (bookedRoom == null)
            {
                ViewBag.ErrorMessage = "No available rooms at this time.";
                return View();
            }

            ViewBag.SuccessMessage = $"Booked {bookedRoom.Room.RName} at {bookedRoom.Date.ToShortDateString()} {bookedRoom.Time}";
            ViewBag.RoomName = bookedRoom.Room.RName;
            ViewBag.BookingDate = bookedRoom.Date.ToShortDateString();
            ViewBag.BookingTime = bookedRoom.Time.ToString();

            return View("Confirmation");
        }

        public IActionResult Confirmation(string date, TimeSpan time, string roomName)
        {
            ViewBag.Date = date;
            ViewBag.Time = time;
            ViewBag.RoomName = roomName;
            return View();
        }




        public IActionResult MyBills()
        {

            var patientId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var bills = AppointmentService.GetMyBills(patientId);

            return View(bills);
        }

        public IActionResult MyAppointments()
        {
            
              var Id = User.FindFirstValue(ClaimTypes.NameIdentifier);

              var appointments = AppointmentService.GetMyAppointments(Id);

               return View(appointments);
            
        }
        public IActionResult MyDoctorAppointments()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);

            var doc = _docServices.GetDoctorByEmail(email);

            var appointments = AppointmentService.GetDoctorMyAppointments(doc.Id);

            return View(appointments);
        }
    }
}
