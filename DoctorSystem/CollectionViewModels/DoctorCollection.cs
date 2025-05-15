using DoctorSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoctorSystem.CollectionViewModels
{
	public class DoctorCollection
	{
		public virtual RegisterViewModel ApplicationUser { get; set; }
		public virtual Doctor Doctor { get; set; }
    }
}