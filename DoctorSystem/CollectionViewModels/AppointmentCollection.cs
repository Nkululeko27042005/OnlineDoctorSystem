﻿using DoctorSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoctorSystem.CollectionViewModels
{
	public class AppointmentCollection
	{
        public virtual Appointment Appointment { get; set; }
        public IEnumerable<Patient> Patients { get; set; }
        public IEnumerable<Doctor> Doctors { get; set; }
    }
}