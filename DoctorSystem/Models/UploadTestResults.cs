using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace DoctorSystem.Models
{
	public class UploadTestResults
	{
		[Key]
		public int Id { get; set; }
        public string PatientName { get; set; }
		public string Gender { get; set; }
		public string Age { get; set; }
		public string VerifierName { get; set; }

		public int TestRequestNumber { get; set; }
        public TestType TestType { get; set; }
		[Display(Name = "Test Type")]
		public int TestTypeId { get; set; }
		public double Result { get; set; }
		public string Unit { get; set; }
		public string NormalRange { get; set; }
		public string ResultStatus { get; set; }
		public string Summary { get; set; }
		public DateTime? Date { get; set; }

        public string CheckPatient()
		{
			ApplicationDbContext db = new ApplicationDbContext();
			var patient = (from d in db.ConfirmCollections
                           where TestRequestNumber == d.TestRequestNumber && d.PatientName == PatientName
                           select d.PatientName).FirstOrDefault();

			return patient;
        }

		public void RemoveConfirmedTest()
		{
			ApplicationDbContext db = new ApplicationDbContext();
            var test = (from d in db.ConfirmCollections
                        where TestRequestNumber == d.TestRequestNumber
                        select d).FirstOrDefault();

            db.ConfirmCollections.Remove(test);
            db.SaveChanges();
        }
    }
}