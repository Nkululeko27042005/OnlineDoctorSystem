using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoctorSystem.Models
{
	public class Reviews
	{
		[Key]
        public int Id { get; set; }
		public Doctor Doctor { get; set; }
        [Display(Name = "Doctor Name")]
		public int DoctorId { get; set; }
		public string Username	{ get; set; }
		[Display(Name = "Give A Rating Out Five")]
		public string Rating { get; set; }
		public string Comment { get; set; }
		public DateTime? Date { get; set; }

    }
}