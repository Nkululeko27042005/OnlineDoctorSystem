using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DoctorSystem.Models
{
	public class CheckIn
	{
		[Key]
		public int Id { get; set; }
		[Required]
		[Display(Name = "Appointment Number")]
        public int AppointmentNumber { get; set; }
		[Required]
		[Display(Name = "Patient Name")]
        public string PatientName { get; set; }
		public DateTime? Date { get; set; }

        public void checkAppointment()
		{
			ApplicationDbContext db = new ApplicationDbContext();
            var app = (from d in db.Appointments
					   where AppointmentNumber == d.Id && PatientName == d.Patient.FullName
                       select d).FirstOrDefault();

			app.Status = "Checked In";
			db.SaveChanges();
        }

	}
}