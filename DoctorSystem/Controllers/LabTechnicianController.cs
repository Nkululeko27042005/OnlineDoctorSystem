using DoctorSystem.CollectionViewModels;
using DoctorSystem.Models;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;

namespace DoctorSystem.Controllers
{
    public class LabTechnicianController : Controller
    {
        private ApplicationDbContext db;
        private ApplicationUserManager _userManager;

        public LabTechnicianController()
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

        // GET: LabTehcnician
        public ActionResult Index(string message)
        {
            ViewBag.Messege = message;
            var model = new CollectionOfAll
            {
                Doctors = db.Doctors.ToList(),
                Patients = db.Patients.ToList(),
            };
            return View(model);
        }

        public ActionResult AddTestType()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddTestType(TestType model)
        {
            var test = new TestType();
            test.TestName = model.TestName;
            test.Price = model.Price;
            db.TestTypes.Add(test);
            db.SaveChanges();
            return RedirectToAction("TestTypeList");
        }

        public ActionResult TestTypeList()
        {
            var Test = db.TestTypes.ToList();
            return View(Test);
        }

        public ActionResult TestRequestList()
        {
            var testRequest = db.TestRequests.Include(c => c.Patient).Include(c => c.Doctor).ToList();
            return View(testRequest);
        }

        public ActionResult ConfirmCollection()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "LabTechnician")]
        public ActionResult ConfirmCollection(ConfirmCollection model)
        {
            var coll = new ConfirmCollection();
            coll.TestRequestNumber = model.TestRequestNumber;
            coll.PatientName = model.PatientName;
            coll.DoctorName = "Dr Mkhululi Manyoni";
            coll.ConfirmationDate = DateTime.UtcNow.Date;
            coll.Cofirmation();
            db.ConfirmCollections.Add(coll);
            db.SaveChanges();
            return RedirectToAction("ConfirmCollectionList");
        }

        public ActionResult ConfirmCollectionList()
        {
            var coll = db.ConfirmCollections.ToList();
            return View(coll);
        }

        [Authorize(Roles = "LabTechnician")]
        public ActionResult UploadTestResults()
        {
            var collection = new UploadTestsCollection()
            {
                UploadTestResults = new UploadTestResults(),
                TestTypes = db.TestTypes.ToList()
            };

            return View(collection);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "LabTechnician")]
        public ActionResult UploadTestResults(UploadTestsCollection model)
        {         
            var patient = (from d in db.ConfirmCollections
                           where model.UploadTestResults.TestRequestNumber == d.TestRequestNumber
                           select d.PatientName).FirstOrDefault();

            var collection = new UploadTestsCollection()
            {
                UploadTestResults = model.UploadTestResults,
                TestTypes = db.TestTypes.ToList()
            };

            var upload = new UploadTestResults();
            upload.PatientName = model.UploadTestResults.PatientName;
            upload.Gender = model.UploadTestResults.Gender;
            upload.Age = model.UploadTestResults.Age;
            upload.VerifierName = model.UploadTestResults.VerifierName;
            upload.TestRequestNumber = model.UploadTestResults.TestRequestNumber;
            upload.TestTypeId = model.UploadTestResults.TestTypeId;
            upload.Result = model.UploadTestResults.Result;
            upload.Unit = model.UploadTestResults.Unit;
            upload.NormalRange = model.UploadTestResults.NormalRange;
            upload.ResultStatus = model.UploadTestResults.ResultStatus;
            upload.Summary = model.UploadTestResults.Summary;
            upload.Date = DateTime.UtcNow.Date;


            if (model.UploadTestResults.PatientName == patient)
            {
                model.UploadTestResults.RemoveConfirmedTest();
                db.UploadTestResults.Add(upload);
                db.SaveChanges();
                return RedirectToAction("TestResultsList");
            }
            else
            {
                ModelState.AddModelError("", "Patient Name and Test Request Number do not match");
                return View(collection);
            }
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

        [Authorize(Roles = "LabTechnician")]
        public ActionResult AddNotification()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "LabTechnician")]
        public ActionResult AddNotification(Notifications model)
        {
            var notification = new Notifications();
            notification.Message = model.Message;
            notification.DateTime = DateTime.UtcNow;
            db.Notifications.Add(notification);
            db.SaveChanges();

            return RedirectToAction("NotificationList");
        }

        public ActionResult NotificationList()
        {
            var notification = db.Notifications.ToList();
            return View(notification);
        }


    }
}