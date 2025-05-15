using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DoctorSystem.Models;

namespace DoctorSystem.CollectionViewModels
{
    public class PrescriptionCollection
    {
        public Prescription Prescription { get; set; }
        public List<Patient> Patients { get; set; }
        public List<Medicine> Medicines { get; set; }
    }
}