using DoctorSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoctorSystem.CollectionViewModels
{
	public class TestRequestCollection
	{
		public TestRequests TestRequests { get; set; }
		public List<Patient> Patients { get; set; }
		public List<Doctor> Doctors { get; set; }
		public List<TestType> TestTypes { get; set; }
	}
}