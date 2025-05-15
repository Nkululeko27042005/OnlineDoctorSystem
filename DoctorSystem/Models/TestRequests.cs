using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoctorSystem.Models
{
	public class TestRequests
	{
		[Key]
		public int Id { get; set; }
		public virtual Doctor Doctor { get; set; }
		[Display(Name = "Doctor Name")]
		public int DoctorId { get; set; }
		public string PracticeNumber { get; set; }
		public string DoctorEmail { get; set; }
		public string DoctorAddress { get; set; }
		public string DoctorTelephone { get; set; }

		public string PatientIdNumber { get; set; }
		public virtual Patient Patient { get; set; }
		[Display(Name = "Patient Name")]
		public int PatientId { get; set;}
		public string Gender { get; set; }
		public int Age { get; set; }
	    public string DoctorRef { get; set; }
		public string TelephoneWork { get; set; }
		public string CellNumber { get; set; }
		public string Email { get; set; }
		public string HomeAddress { get; set; }
		public string CollectionSite { get; set; }
		public string CollectionDate { get; set; }
		public string CollectionTime { get; set; }
		public string CollectedBy { get; set; }
		public string SpecialRequest { get; set; } 

		public virtual TestType TestType { get; set; }
		[Display(Name = "Test Type")]
		public int TestTypeId { get; set; }

		public virtual TestType TestType2 { get; set; }
        [Display(Name = "Test Type")]
        public int TestTypeId2 { get; set; }
		public virtual TestType TestType3 { get; set; }
        [Display(Name = "Test Type")]
        public int TestTypeId3 { get; set; }

		public virtual TestType TestType4 { get; set; }
        [Display(Name = "Test Type")]
        public int TestTypeId4 { get; set; }
		public virtual TestType TestType5 { get; set; }
        [Display(Name = "Test Type")]
        public int TestTypeId5 { get; set; }
		public virtual TestType TestType6 { get; set; }
        [Display(Name = "Test Type")]
        public int TestTypeId6 { get; set; }
		public virtual TestType TestType7 { get; set; }
        [Display(Name = "Test Type")]
        public int TestTypeId7 { get; set; }
		public virtual TestType TestType8 { get; set; }
        [Display(Name = "Test Type")]
        public int TestTypeId8 { get; set; }
		public virtual TestType TestType9 { get; set; }
        [Display(Name = "Test Type")]
        public int TestTypeId9 { get; set; }

		public virtual TestType TestType10 { get; set; }
        [Display(Name = "Test Type")]
        public int TestTypeId10 { get; set; }
		public virtual TestType TestType11 { get; set; }
        [Display(Name = "Test Type")]
        public int TestTypeId11 { get; set; }
		public virtual TestType TestType12 { get; set; }
        [Display(Name = "Test Type")]
        public int TestTypeId12 { get; set; }

		public string SpecimenTaken { get; set; }
		public string ICD10 { get; set; }

		public string Status { get; set; }

		public DateTime? Date {  get; set; }

	}
}