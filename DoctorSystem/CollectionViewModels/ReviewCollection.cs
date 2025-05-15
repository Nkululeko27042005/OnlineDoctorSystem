using DoctorSystem.Models;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoctorSystem.CollectionViewModels
{
	public class ReviewCollection
	{
		public Reviews Reviews { get; set; }
		public List<Doctor> Doctors { get; set; }
    }
}