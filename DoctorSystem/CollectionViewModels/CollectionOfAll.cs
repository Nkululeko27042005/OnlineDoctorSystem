using DoctorSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoctorSystem.CollectionViewModels
{
	public class CollectionOfAll
	{
		public IEnumerable <Patient> Patients { get; set; }
		public IEnumerable <Doctor> Doctors { get; set; }
		public IEnumerable <Appointment> Appointments  { get; set; }
        public IEnumerable<Appointment> CompletedAppointments { get; set; }
        public IEnumerable<Appointment> PendingAppointments { get; set; }
        public IEnumerable<Diagnosis> Diagnoses { get; set; }
        public IEnumerable<Medicine> Medicines { get; set; }
        public IEnumerable<Prescription> Prescriptions { get; set; }
        public IEnumerable<TestType> Tests { get; set; }
        public IEnumerable<TestRequests> TestRequests { get; set; }
        public IEnumerable<UploadTestResults> UploadTestResults { get; set; }
        public IEnumerable<Notifications> Notifications { get; set; }
        public IEnumerable<Reviews> Reviews { get; set; }
    }
}