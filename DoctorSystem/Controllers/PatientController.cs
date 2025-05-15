using DoctorSystem.Models;
using DoctorSystem.CollectionViewModels;
using Microsoft.AspNet.Identity;
using System.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;
using System.Net;

namespace DoctorSystem.Controllers
{
    public class PatientController : Controller
    {
        private ApplicationDbContext db;

        public PatientController()
        {
            db = new ApplicationDbContext();
        }

        // GET: Patient
        protected override void Dispose(bool dispsoing)
        {
            db.Dispose();
        }

        public ActionResult DiagnosisList()
        {
            string user = User.Identity.GetUserId();
            var patient = db.Patients.Single(c => c.ApplicationUserId == user);
            var diagnoses = db.Diagnoses.Include(d => d.patient).Where(c =>c.PatientId == patient.Id).ToList();
            return View(diagnoses);
        }

        public ActionResult DiagnosisDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Diagnosis diagnosis = db.Diagnoses.Find(id);
            if (diagnosis == null)
            {
                return HttpNotFound();
            }
            return View(diagnosis);
        }

        public ActionResult PrescriptionList()
        {
            string user = User.Identity.GetUserId();
            var patient = db.Patients.Single(c => c.ApplicationUserId == user);
            var prescription = db.Prescriptions.Include(c => c.Patient).Where(c => c.PatientId == patient.Id).ToList();
            return View(prescription);
        }

        public ActionResult PrescriptionView(int id)
        {
            var prescription = db.Prescriptions.Single(c => c.Id == id);
            return View(prescription);
        }

        [Authorize(Roles = "Patient")]
        public ActionResult cancelAppointments()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult cancelAppointments(CancelAppointment c)
        {
            c.cancelAppointment();
            return RedirectToAction("PendingAppointments");
        }

        private List<SelectListItem> GetTimeSlots(int doctorId, DateTime date)
        {
            var slots = new List<SelectListItem>();
            var start = new TimeSpan(9, 0, 0);
            var end = new TimeSpan(17, 0, 0);
            var interval = TimeSpan.FromMinutes(30);

            // Get booked slots for this doctor on the given date
            var bookedTimes = db.Appointments
                .Where(a => a.DoctorId == doctorId && a.AppointmentDate == date)
                .Select(a => a.AppointmentTime)
                .ToList();

            while (start < end)
            {
                if (!bookedTimes.Contains(start))
                {
                    slots.Add(new SelectListItem
                    {
                        Value = start.ToString(@"hh\:mm"),
                        Text = DateTime.Today.Add(start).ToString("hh:mm tt")
                    });
                }
                start = start.Add(interval);
            }

            return slots;
        }


        [Authorize(Roles = "Patient")]
        public ActionResult Index(string message)
        {
            ViewBag.Messege = message;
            string user = User.Identity.GetUserId();
            var patient = db.Patients.Single(c => c.ApplicationUserId == user);
            var date = DateTime.Now.Date;
            var model = new CollectionOfAll
            {
                Doctors = db.Doctors.ToList(),
                Patients = db.Patients.ToList(),
                CompletedAppointments = db.Appointments.Where(c => c.Status == "Checked In").Where(c => c.PatientId == patient.Id).Where(c => c.AppointmentDate >= date).ToList(),
                PendingAppointments = db.Appointments.Where(c => c.Status == "Pending").Where(c => c.PatientId == patient.Id).Where(c => c.AppointmentDate >= date).ToList(),
            };
            return View(model);
        }

        [Authorize(Roles = "Patient")]
        public ActionResult UpdateProfile(string id)
        {
            var patient = db.Patients.Single(c => c.ApplicationUserId == id);
            return View(patient);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateProfile(string id, Patient model)
        {
            var patient = db.Patients.Single(c => c.ApplicationUserId == id);
            patient.FirstName = model.FirstName;
            patient.LastName = model.LastName;
            patient.FullName = model.FirstName + " " + model.LastName;
            patient.Contact = model.Contact;
            patient.Address = model.Address;
            patient.BloodGroup = model.BloodGroup;
            patient.Gender = model.Gender;
            db.SaveChanges();
            return View();
        }

        [Authorize(Roles = "Patient")]
        public ActionResult AddAppointment()
        {
            var today = DateTime.Today;
            var defaultDoctorId = db.Doctors.FirstOrDefault()?.Id ?? 0;

            var collection = new AppointmentCollection
            {
                Appointment = new Appointment { AppointmentDate = today},
                Doctors = db.Doctors.ToList()
            };

            ViewBag.TimeSlots = GetTimeSlots(defaultDoctorId, today);

            ViewBag.TimeSlot = new List<SelectListItem>
            {
              new SelectListItem { Value = "09:00", Text = "9:00 AM" },
              new SelectListItem { Value = "10:00", Text = "10:00 AM" },
              new SelectListItem { Value = "11:00", Text = "11:00 AM" },
              new SelectListItem { Value = "12:00", Text = "12:00 PM" },
              new SelectListItem { Value = "13:00", Text = "1:00 PM" },
              new SelectListItem { Value = "14:00", Text = "2:00 PM" },
              new SelectListItem { Value = "15:00", Text = "3:00 PM" },
              new SelectListItem { Value = "16:00", Text = "4:00 PM" },
              new SelectListItem { Value = "17:00", Text = "5:00 PM" },
            };

            return View(collection);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddAppointment(AppointmentCollection model)
        {
            var collection = new AppointmentCollection
            {
                Appointment = model.Appointment,
                Doctors = db.Doctors.ToList() ?? new List<Doctor>()

            };

            ViewBag.TimeSlots = GetTimeSlots(model.Appointment.DoctorId, model.Appointment.AppointmentDate ?? DateTime.Today);

            if (model.Appointment.AppointmentDate >= DateTime.Now.Date)
            {
                string user = User.Identity.GetUserId();
                var patient = db.Patients.Single(c => c.ApplicationUserId == user);
                var appointment = new Appointment
                {
                    PatientId = patient.Id,
                    DoctorId = model.Appointment.DoctorId,
                    AppointmentDate = model.Appointment.AppointmentDate,
                    AppointmentTime = model.Appointment.AppointmentTime,
                    Problem = model.Appointment.Problem,
                    Status = "Pending"
                };
         

                db.Appointments.Add(appointment);
                db.SaveChanges();
                return RedirectToAction("PendingAppointments", "Patient");
            }

            ViewBag.Message = "Date must be greater than today";
            return View(collection);
        }

        public ActionResult PendingAppointments()
        {
            string user = User.Identity.GetUserId();
            var patient = db.Patients.Single(c => c.ApplicationUserId == user);
            // Replace the problematic line in the ListOfAppointments method with the following:
            var appointment = db.Appointments.Include(c => c.Doctor).Where(c => c.PatientId == patient.Id).Where(c => c.Status == "Pending").ToList();
            return View(appointment);
        }
        
        public ActionResult CompletedAppointments()
        {
            string user = User.Identity.GetUserId();
            var patient = db.Patients.Single(c => c.ApplicationUserId == user);
            // Replace the problematic line in the ListOfAppointments method with the following:
            var appointment = db.Appointments.Include(c => c.Doctor).Where(c => c.PatientId == patient.Id).Where(c => c.Status == "Checked In" ).ToList();
            return View(appointment);
        }

        [Authorize(Roles = "Patient")]
        public ActionResult DeleteAppointment(int? id)
        {
            var appointment = db.Appointments.Single(c => c.Id == id);
            return View(appointment);
        }

        [HttpPost, ActionName("DeleteAppointment")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteAppointment(int id)
        {
            var appointment = db.Appointments.Single(c => c.Id == id);
            db.Appointments.Remove(appointment);
            db.SaveChanges();
            return RedirectToAction("ListOfAppointments");
        }

        [Authorize(Roles = "Patient")]
         public ActionResult MakeReview()
         {
            var collection = new ReviewCollection
            {
                Reviews = new Reviews(),
                Doctors = db.Doctors.ToList()
            };
            return View(collection);
         }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Patient")]
        public ActionResult MakeReview(ReviewCollection model)
        {
            string user = User.Identity.GetUserId();
            var patient = db.Patients.Single(c => c.ApplicationUserId == user);
            var collection = new ReviewCollection
            {
                Reviews = model.Reviews,
                Doctors = db.Doctors.ToList()
            };

            var review = new Reviews()
            {
                DoctorId = model.Reviews.DoctorId,
                Username = patient.FullName,
                Rating = model.Reviews.Rating,
                Comment = model.Reviews.Comment,
                Date = DateTime.UtcNow.Date
            };

            db.Reviews.Add(review);
            db.SaveChanges();
            return RedirectToAction("ReviewList");
        }

        public ActionResult ReviewList()
        {
            var reviews = db.Reviews.Include(c => c.Doctor).ToList();
            return View(reviews);
        }
    }
}