using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoctorSystem.Models
{
	public class Notifications
	{
		public int Id { get; set; }
        public string Message { get; set; }
        public DateTime? DateTime { get; set; }
    }
}