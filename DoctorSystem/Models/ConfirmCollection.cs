using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DoctorSystem.Models
{
	public class ConfirmCollection
	{
		[Key]
		public int Id { get; set; }
		[Display(Name = "Test Request Number")]
		public int TestRequestNumber { get; set; }
		[Display(Name = "Patient Name")]
		public string PatientName { get; set; }
		public string DoctorName { get; set; }
		public DateTime? ConfirmationDate { get; set; }

		public void Cofirmation()
		{
			ApplicationDbContext db = new ApplicationDbContext();
			var coll = (from d in db.TestRequests
						where TestRequestNumber == d.Id && PatientName == d.Patient.FullName
						select d).FirstOrDefault();

			coll.Status = "Received";
			db.SaveChanges();
		}
	}
}