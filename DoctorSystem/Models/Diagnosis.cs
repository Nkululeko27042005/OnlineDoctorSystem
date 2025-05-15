using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DoctorSystem.Models
{
	public class Diagnosis
	{
		[Key]
		public int Id { get; set; }
		public string DoctorName { get; set; }
        public virtual Patient patient { get; set; }
		public int PatientId { get; set; }
		[Required]
		[Display(Name = "Diagnosis")]
		public string Diagnosistic { get; set; }
		[Display(Name = "Additional Notes")]
		public string AdditionalNotes { get; set; }
		public DateTime? Date { get; set; }
    }
}