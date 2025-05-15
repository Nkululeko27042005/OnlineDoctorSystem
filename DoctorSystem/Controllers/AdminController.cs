using DoctorSystem.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DoctorSystem.CollectionViewModels;

namespace DoctorSystem.Controllers
{
    public class AdminController : Controller
    {
        private ApplicationDbContext db;
        private ApplicationUserManager _userManager;

        public AdminController()
        {
            db = new ApplicationDbContext();
        }

        public ApplicationUserManager UserManager
        {
            get { return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>(); }
            private set { _userManager = value; }
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
        }

        // GET: Admin
        public ActionResult Index(string message)
        {
            var date = DateTime.Now.Date;
            ViewBag.Messege = message;
            var model = new CollectionOfAll
            {
                Doctors = db.Doctors.ToList(),
                Patients = db.Patients.ToList(),
                CompletedAppointments = db.Appointments.Where(c => c.Status == "Checked In").Where(c => c.AppointmentDate >= date).ToList(),
                PendingAppointments = db.Appointments.Where(c => c.Status == "Pending").Where(c => c.AppointmentDate >= date).ToList(),
            };
            return View(model);
        }

        public ActionResult CompletedAppointments()
        {
            var date = DateTime.Now.Date;
            var appointment = db.Appointments.Include(c => c.Doctor).Include(c => c.Patient).Where(c => c.Status == "Checked In").Where(c => c.AppointmentDate >= date).ToList();
            return View(appointment);
        }

        //List of Pending Appointments
        public ActionResult PendingAppointments()
        {
            var date = DateTime.Now.Date;
            var appointment = db.Appointments.Include(c => c.Doctor).Include(c => c.Patient).Where(c => c.Status == "Pending").Where(c => c.AppointmentDate >= date).ToList();
            return View(appointment);
        }

        public ActionResult CheckingIn()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CheckingIn(CheckIn model)
        {
            var check = new CheckIn();
            check.PatientName = model.PatientName;
            check.AppointmentNumber = model.AppointmentNumber;
            check.Date = DateTime.Now.Date;
            model.checkAppointment();
            db.checkIns.Add(check);
            db.SaveChanges();
            return RedirectToAction("CompletedAppointments", new { message = "Check In Successfull" });
        }

        public ActionResult AddMedicine()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddMedicine(Medicine model)
        {
            var medicine = new Medicine();
            medicine.Name = model.Name;
            db.Medicines.Add(medicine);
            db.SaveChanges();
            return RedirectToAction("MedicineList");
        }

        public ActionResult MedicineList()
        {
            var medicine = db.Medicines.ToList();
            return View(medicine);
        }

        public ActionResult ReviewList()
        {
            var reviews = db.Reviews.Include(c => c.Doctor).ToList();
            return View(reviews);
        }
    }
}