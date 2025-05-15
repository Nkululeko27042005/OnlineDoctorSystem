using DoctorSystem.CollectionViewModels;
using DoctorSystem.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.Owin;

namespace DoctorSystem.Controllers
{
    public class DoctorController : Controller
    {
        private ApplicationDbContext db;
        private ApplicationUserManager _userManager;
        public DoctorController()
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

        // GET: Doctor
        [Authorize(Roles = "Doctor")]
        public ActionResult Index(string message)
        {
            var date = DateTime.Now.Date;
            ViewBag.Messege = message;
            var user = User.Identity.GetUserId();
            var doctor = db.Doctors.FirstOrDefault(d => d.ApplicationUserId == user);
            var model = new CollectionOfAll
            {
                Doctors = db.Doctors.ToList(),
                Patients = db.Patients.ToList(),
                Notifications = db.Notifications.ToList() ?? new List<Notifications>(),
                CompletedAppointments = db.Appointments.Where(c => c.DoctorId == doctor.Id).Where(c => c.Status == "Checked In").Where(c => c.AppointmentDate >= date).ToList(),
                PendingAppointments = db.Appointments.Where(c => c.DoctorId == doctor.Id).Where(c => c.Status == "Pending").Where(c => c.AppointmentDate >= date).ToList(),
            };
            return View(model);
        }

        [Authorize(Roles = "Doctor")]
        public ActionResult AddPrescription()
        {
            var collection = new PrescriptionCollection
            {
                Prescription = new Prescription(),
                Patients = db.Patients.ToList(),
                Medicines = db.Medicines.ToList()
            };

            return View(collection);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddPrescription(PrescriptionCollection model)
        {
            var collection = new PrescriptionCollection
            {
                Prescription = model.Prescription,
                Patients = db.Patients.ToList(),
                Medicines = db.Medicines.ToList()
            };

            var prescription = new Prescription();
            prescription.DoctorName = "Dr Mkhululi Manyoni";
            prescription.PatientId = model.Prescription.PatientId;
            prescription.MedicineId = model.Prescription.MedicineId;
            prescription.MedicineId2 = model.Prescription.MedicineId2;
            prescription.MedicineId3 = model.Prescription.MedicineId3;
            prescription.MedicineId4 = model.Prescription.MedicineId4;
            prescription.MedicineId5 = model.Prescription.MedicineId5;
            prescription.MedicineId6 = model.Prescription.MedicineId6;
            prescription.MedicineId7 = model.Prescription.MedicineId7;
            prescription.MedicineId8 = model.Prescription.MedicineId8;
            prescription.MedicineId9 = model.Prescription.MedicineId9;
            prescription.MedicineId10 = model.Prescription.MedicineId10;
            prescription.Dosage = model.Prescription.Dosage;
            prescription.Dosage2 = model.Prescription.Dosage2;
            prescription.Dosage3 = model.Prescription.Dosage3;
            prescription.Dosage4 = model.Prescription.Dosage4;
            prescription.Dosage5 = model.Prescription.Dosage5;
            prescription.Dosage6 = model.Prescription.Dosage6;
            prescription.Dosage7 = model.Prescription.Dosage7;
            prescription.Dosage8 = model.Prescription.Dosage8;
            prescription.Dosage9 = model.Prescription.Dosage9;
            prescription.Dosage10 = model.Prescription.Dosage10;
            prescription.Morning = model.Prescription.Morning;
            prescription.Morning2 = model.Prescription.Morning2;
            prescription.Morning3 = model.Prescription.Morning3;
            prescription.Morning4 = model.Prescription.Morning4;
            prescription.Morning5 = model.Prescription.Morning5;
            prescription.Morning6 = model.Prescription.Morning6;
            prescription.Morning7 = model.Prescription.Morning7;
            prescription.Morning8 = model.Prescription.Morning8;
            prescription.Morning9 = model.Prescription.Morning9;
            prescription.Morning10 = model.Prescription.Morning10;
            prescription.Afternoon = model.Prescription.Afternoon;
            prescription.Afternoon2 = model.Prescription.Afternoon2;
            prescription.Afternoon3 = model.Prescription.Afternoon3;
            prescription.Afternoon4 = model.Prescription.Afternoon4;
            prescription.Afternoon5 = model.Prescription.Afternoon5;
            prescription.Afternoon6 = model.Prescription.Afternoon6;
            prescription.Afternoon7 = model.Prescription.Afternoon7;
            prescription.Afternoon8 = model.Prescription.Afternoon8;
            prescription.Afternoon9 = model.Prescription.Afternoon9;
            prescription.Afternoon10 = model.Prescription.Afternoon10;
            prescription.Evening = model.Prescription.Evening;
            prescription.Evening2 = model.Prescription.Evening2;
            prescription.Evening3 = model.Prescription.Evening3;
            prescription.Evening4 = model.Prescription.Evening4;
            prescription.Evening5 = model.Prescription.Evening5;
            prescription.Evening6 = model.Prescription.Evening6;
            prescription.Evening7 = model.Prescription.Evening7;
            prescription.Evening8 = model.Prescription.Evening8;
            prescription.Evening9 = model.Prescription.Evening9;
            prescription.Evening10 = model.Prescription.Evening10;
            prescription.CheckUp = model.Prescription.CheckUp;
            prescription.Date = DateTime.Now.Date;
            db.Prescriptions.Add(prescription);
            db.SaveChanges();
            return RedirectToAction("PrescriptionList");
        }

        public ActionResult PrescriptionView(int id)
        {
            var prescription = db.Prescriptions.Single(c => c.Id == id);
            return View(prescription);
        }

        [Authorize(Roles = "Doctor")]
        public ActionResult PrescriptionList()
        {
            var prescription = db.Prescriptions.Include(c => c.Patient).ToList();
            return View(prescription);
        }

        [Authorize(Roles = "Doctor")]
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

        [Authorize(Roles = "Doctor")]
        public ActionResult MedicineList()
        {
          var medicine = db.Medicines.ToList();
            return View(medicine);
        }

        [Authorize(Roles = "Doctor")]
        public ActionResult AddDiagnosis()
        {
            var collection = new DiagnosisCollection
            {
                Diagnosis = new Diagnosis(),
                Patients = db.Patients.ToList()
            };

            return View(collection);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddDiagnosis(DiagnosisCollection model)
        {
            var collection = new DiagnosisCollection
            {
                Diagnosis = model.Diagnosis,
                Patients = db.Patients.ToList(),
            };
            var diagnosis = new Diagnosis();
            diagnosis.DoctorName = "Dr Mkhululi Manyoni";
            diagnosis.PatientId = model.Diagnosis.PatientId;
            diagnosis.Diagnosistic = model.Diagnosis.Diagnosistic;
            diagnosis.AdditionalNotes = model.Diagnosis.AdditionalNotes;
            diagnosis.Date = DateTime.Now.Date;
            db.Diagnoses.Add(diagnosis);
            db.SaveChanges();
            return RedirectToAction("DiagnosisList");

        }

        public ActionResult DiagnosisList()
        {
            var diagnoses = db.Diagnoses.Include(d => d.patient).ToList();
            return View(diagnoses);
        }

        [Authorize(Roles = "Doctor")]
        public ActionResult CheckingIn()
        {
            return View();
        }

        [Authorize(Roles = "Doctor")]
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
            return RedirectToAction("CompletedAppointments", new { message = "Check In Successfully" });
        }

        public ActionResult CompletedAppointments()
        {
            var user = User.Identity.GetUserId();
            var doctor = db.Doctors.FirstOrDefault(c => c.ApplicationUserId == user);
            var date = DateTime.Now.Date;
            var appointment = db.Appointments.Include(c => c.Doctor).Include(c => c.Patient).Where(c => c.Status == "Checked In").Where(c => c.AppointmentDate >= date).ToList();
            return View(appointment);
        }

        //List of Pending Appointments
        public ActionResult PendingAppointments()
        {
            var user = User.Identity.GetUserId();
            var doctor = db.Doctors.FirstOrDefault();
            var date = DateTime.Now.Date;
            var appointment = db.Appointments.Include(c => c.Doctor).Include(c => c.Patient).Where(c => c.Status == "Pending").Where(c => c.AppointmentDate >= date).ToList();
            return View(appointment);
        }

        [Authorize(Roles = "Doctor")]
        public ActionResult EditAppointment(int id)
        {
            var collection = new AppointmentCollection
            {
                Appointment = db.Appointments.Single(c => c.Id == id),
                Patients = db.Patients.ToList()
            };
            return View(collection);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditAppointment(int id, AppointmentCollection model)
        {
            var collection = new AppointmentCollection
            {
                Appointment = model.Appointment,
                Patients = db.Patients.ToList()
            };
            if (model.Appointment.AppointmentDate >= DateTime.Now.Date)
            {
                var appointment = db.Appointments.Single(c => c.Id == id);
                appointment.PatientId = model.Appointment.PatientId;
                appointment.AppointmentDate = model.Appointment.AppointmentDate;
                appointment.AppointmentTime = model.Appointment.AppointmentTime;
                appointment.Problem = model.Appointment.Problem;
                appointment.Status = model.Appointment.Status;
                db.SaveChanges();
                if (model.Appointment.Status == "Completed")
                {
                    return RedirectToAction("ActiveAppointments");
                }
                else
                {
                    return RedirectToAction("PendingAppointments");
                }
            }

            ViewBag.Messege = "Please Enter the Date greater than today or equal!!";

            return View(collection);
        }

        // GET: Doctor/AddDoctor
        public ActionResult AddDoctor()
        {
            var collection = new DoctorCollection
            {
                ApplicationUser = new RegisterViewModel(),
                Doctor = new Doctor(),
            };
            return View(collection);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddDoctor(DoctorCollection model)
        {
            var user = new ApplicationUser
            {
                UserName = model.ApplicationUser.UserName,
                Email = model.ApplicationUser.Email,
                UserRole = "Doctor",
                RegisteredDate = DateTime.Now.Date
            };

            var result = await UserManager.CreateAsync(user, model.ApplicationUser.Password);
            if (result.Succeeded)
            {
                await UserManager.AddToRoleAsync(user.Id, "Doctor");
                var doctor = new Doctor
                {
                    FirstName = model.Doctor.FirstName,
                    LastName = model.Doctor.LastName,
                    FullName = "Dr. " + model.Doctor.FirstName + " " + model.Doctor.LastName,
                    EmailAddress = model.ApplicationUser.Email,
                    ContactNo = model.Doctor.ContactNo,
                    PhoneNo = model.Doctor.PhoneNo,
                    Education = model.Doctor.Education,
                    Specialization = model.Doctor.Specialization,
                    Gender = model.Doctor.Gender,
                    ApplicationUserId = user.Id,
                    Address = model.Doctor.Address,
                };
                db.Doctors.Add(doctor); 
                db.SaveChanges();
                return RedirectToAction("ListOfDoctors");
            }

            return HttpNotFound();
           
        }

        [Authorize(Roles = "Doctor")]
        public ActionResult ListOfDoctors()
        {
            var doctor = db.Doctors.ToList();
            return View(doctor);
        }

        //Detail of Doctor
        [Authorize(Roles = "Doctor")]
        public ActionResult DoctorDetail(int id)
        {
            var doctor = db.Doctors.SingleOrDefault(c => c.Id == id);
            return View(doctor);
        }

        [Authorize(Roles = "Doctor")]
        public ActionResult EditDoctors(int id)
        {
            var collection = new DoctorCollection
            {
                Doctor = db.Doctors.Single(c => c.Id == id)
            };
            return View(collection);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditDoctors(int id, DoctorCollection model)
        {
            var doctor = db.Doctors.Single(c => c.Id == id);
            doctor.FirstName = model.Doctor.FirstName;
            doctor.LastName = model.Doctor.LastName;
            doctor.FullName = "Dr. " + model.Doctor.FirstName + " " + model.Doctor.LastName;
            doctor.ContactNo = model.Doctor.ContactNo;
            doctor.PhoneNo = model.Doctor.PhoneNo;
            doctor.Education = model.Doctor.Education;
            doctor.Specialization = model.Doctor.Specialization;
            doctor.Gender = model.Doctor.Gender;
            doctor.Address = model.Doctor.Address;
            db.SaveChanges();

            return RedirectToAction("ListOfDoctors");
        }

        //Delete Doctor
        [Authorize(Roles = "Doctor")]
        public ActionResult DeleteDoctor(string id)
        {
            var UserId = db.Doctors.Single(c => c.ApplicationUserId == id);
            return View(UserId);
        }

        [HttpPost, ActionName("DeleteDoctor")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteDoctor(string id, Doctor model)
        {
            var doctor = db.Doctors.Single(c => c.ApplicationUserId == id);
            var user = db.Users.Single(c => c.Id == id);
            db.Users.Remove(user);
            db.Doctors.Remove(doctor);
            db.SaveChanges();
            return RedirectToAction("ListOfDoctors");
        }


        public ActionResult AddTestRequest()
        {
            var collection = new TestRequestCollection
            {
                TestRequests = new TestRequests(),
                Patients = db.Patients.ToList(),
                Doctors = db.Doctors.ToList(),
                TestTypes = db.TestTypes.ToList()
            };

            return View(collection);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Doctor")]
        public ActionResult AddTestRequest(TestRequestCollection model)
        {
            var collection = new TestRequestCollection
            {
                TestRequests = model.TestRequests,
                Patients = db.Patients.ToList(),
                Doctors = db.Doctors.ToList(),
                TestTypes = db.TestTypes.ToList()
            };

            var testRequest = new TestRequests();
            testRequest.DoctorId = model.TestRequests.DoctorId;
            testRequest.PracticeNumber = model.TestRequests.PracticeNumber;
            testRequest.DoctorEmail = model.TestRequests.DoctorEmail;
            testRequest.DoctorAddress = model.TestRequests.DoctorAddress;
            testRequest.DoctorTelephone = model.TestRequests.DoctorTelephone;
            testRequest.PatientIdNumber = model.TestRequests.PatientIdNumber;
            testRequest.PatientId = model.TestRequests.PatientId;
            testRequest.Gender = model.TestRequests.Gender;
            testRequest.Age = model.TestRequests.Age;
            testRequest.DoctorRef = model.TestRequests.DoctorRef;
            testRequest.TelephoneWork = model.TestRequests.TelephoneWork;
            testRequest.CellNumber = model.TestRequests.CellNumber;
            testRequest.Email = model.TestRequests.Email;
            testRequest.HomeAddress = model.TestRequests.HomeAddress;
            testRequest.CollectionSite = model.TestRequests.CollectionSite;
            testRequest.CollectionDate = model.TestRequests.CollectionDate;
            testRequest.CollectionTime = model.TestRequests.CollectionTime;
            testRequest.CollectedBy = model.TestRequests.CollectedBy;
            testRequest.SpecialRequest = model.TestRequests.SpecialRequest;

            testRequest.TestTypeId = model.TestRequests.TestTypeId;
            testRequest.TestTypeId2 = model.TestRequests.TestTypeId2;
            testRequest.TestTypeId3 = model.TestRequests.TestTypeId3;
            testRequest.TestTypeId4 = model.TestRequests.TestTypeId4;
            testRequest.TestTypeId5 = model.TestRequests.TestTypeId5;
            testRequest.TestTypeId6 = model.TestRequests.TestTypeId6;
            testRequest.TestTypeId7 = model.TestRequests.TestTypeId7;
            testRequest.TestTypeId8 = model.TestRequests.TestTypeId8;
            testRequest.TestTypeId9 = model.TestRequests.TestTypeId9;
            testRequest.TestTypeId10 = model.TestRequests.TestTypeId10;
            testRequest.TestTypeId11 = model.TestRequests.TestTypeId11;
            testRequest.TestTypeId12 = model.TestRequests.TestTypeId12;

            testRequest.TestType = model.TestRequests.TestType;
            testRequest.TestType2 = model.TestRequests.TestType2;
            testRequest.TestType3 = model.TestRequests.TestType3;
            testRequest.TestType4 = model.TestRequests.TestType4;
            testRequest.TestType5 = model.TestRequests.TestType5;
            testRequest.TestType6 = model.TestRequests.TestType6;
            testRequest.TestType7 = model.TestRequests.TestType7;
            testRequest.TestType8 = model.TestRequests.TestType8;
            testRequest.TestType9 = model.TestRequests.TestType9;
            testRequest.TestType10 = model.TestRequests.TestType10;
            testRequest.TestType11 = model.TestRequests.TestType11;
            testRequest.TestType12 = model.TestRequests.TestType12;

            testRequest.SpecimenTaken = model.TestRequests.SpecimenTaken;
            testRequest.ICD10 = model.TestRequests.ICD10;
            testRequest.Date = DateTime.Now.Date;
            testRequest.Status = "Not Received";

            db.TestRequests.Add(testRequest);
            db.SaveChanges();
            return RedirectToAction("TestRequestList");
        }

        public ActionResult TestRequestList()
        {
            var testRequest = db.TestRequests.Include(c => c.Patient).Include(c => c.Doctor).ToList();
            return View(testRequest);
        }

        public ActionResult TestRequestDetails(int id)
        {
            var test = db.TestRequests.Find(id);
            return View(test);
        }

        public ActionResult TestResultsList()
        {
            var test = db.UploadTestResults.ToList();
            return View(test);
        }

        public ActionResult TestResultsDetails(int id)
        {
            var test = db.UploadTestResults.Find(id);
            return View(test);
        }

        public ActionResult ConfirmCollectionList()
        {
            var coll = db.ConfirmCollections.ToList();
            return View(coll);
        }

        [Authorize(Roles = "Doctor")]
        public ActionResult NotificationList()
        {
            var notification = db.Notifications.ToList();
            return View(notification);
        }

        public ActionResult ReviewList()
        {
            var reviews = db.Reviews.Include(c => c.Doctor).ToList();
            return View(reviews);
        }
    }
}