using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DoctorSystem.Models
{
	public class TestType
	{
		[Key]
		public int Id { get; set; }
		public string TestName { get; set; }
		public long Price { get; set; }
	}
}