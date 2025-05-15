using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoctorSystem.Models
{
	public class CancelAppointment
	{
		public int AppointmentNumber { get; set; }
		public string FullName { get; set; }

		public void cancelAppointment()
		{
			ApplicationDbContext db = new ApplicationDbContext();
			var c = (from d in db.Appointments
					 where AppointmentNumber == d.Id && d.Status == "Pending"
					 select d).FirstOrDefault();

			db.Appointments.Remove(c);
			db.SaveChanges();
        }
	}
}