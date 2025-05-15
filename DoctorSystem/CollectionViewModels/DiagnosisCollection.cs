using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DoctorSystem.Models;

namespace DoctorSystem.CollectionViewModels
{
	public class DiagnosisCollection
	{
		public Diagnosis Diagnosis { get; set; }
		public IEnumerable<Patient> Patients { get; set; }
	}
}