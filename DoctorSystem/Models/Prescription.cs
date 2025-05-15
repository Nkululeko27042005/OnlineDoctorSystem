using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DoctorSystem.Models
{
	public class Prescription
	{
		[Key]
		public int Id { get; set; }
        public string DoctorName { get; set; }
        public virtual Patient Patient { get; set; }
        [Display(Name = "Patient Name")]
        public int PatientId { get; set; }
        public virtual Medicine Medicine { get; set; }
        [Required]
        [Display(Name = "Medicine ")]
		public int MedicineId { get; set; }
        public string Dosage { get; set; }
        public bool Morning { get; set; }
        public bool Afternoon { get; set; }
        public bool Evening { get; set; }

        public virtual Medicine Medicine2 { get; set; }
        [Display(Name = "Medicine ")]
        public int MedicineId2 { get; set; }
        public string Dosage2 { get; set; }
        public bool Morning2 { get; set; }
        public bool Afternoon2 { get; set; }
        public bool Evening2 { get; set; }

        public virtual Medicine Medicine3 { get; set; }
        [Display(Name = "Medicine ")]
        public int MedicineId3 { get; set; }
        public string Dosage3 { get; set; }
        public bool Morning3 { get; set; }
        public bool Afternoon3 { get; set; }
        public bool Evening3 { get; set; }

        public virtual Medicine Medicine4 { get; set; }
        [Display(Name = "Medicine ")]
        public int MedicineId4 { get; set; }
        public string Dosage4 { get; set; }
        public bool Morning4 { get; set; }
        public bool Afternoon4 { get; set; }
        public bool Evening4 { get; set; }

        public virtual Medicine Medicine5 { get; set; }
        [Display(Name = "Medicine ")]
        public int MedicineId5 { get; set; }
        public string Dosage5 { get; set; }
        public bool Morning5 { get; set; }
        public bool Afternoon5 { get; set; }
        public bool Evening5 { get; set; }

        public virtual Medicine Medicine6 { get; set; }
        [Display(Name = "Medicine ")]
        public int MedicineId6 { get; set; }
        public string Dosage6 { get; set; }
        public bool Morning6 { get; set; }
        public bool Afternoon6 { get; set; }
        public bool Evening6 { get; set; }

        public virtual Medicine Medicine7 { get; set; }
        [Display(Name = "Medicine ")]
        public int MedicineId7 { get; set; }
        public string Dosage7 { get; set; }
        public bool Morning7 { get; set; }
        public bool Afternoon7 { get; set; }
        public bool Evening7 { get; set; }

        public virtual Medicine Medicine8 { get; set; }
        [Display(Name = "Medicine ")]
        public int MedicineId8 { get; set; }
        public string Dosage8 { get; set; }
        public bool Morning8 { get; set; }
        public bool Afternoon8 { get; set; }
        public bool Evening8 { get; set; }

        public virtual Medicine Medicine9 { get; set; }
        [Display(Name = "Medicine ")]
        public int MedicineId9 { get; set; }
        public string Dosage9 { get; set; }
        public bool Morning9 { get; set; }
        public bool Afternoon9 { get; set; }
        public bool Evening9 { get; set; }

        public virtual Medicine Medicine10 { get; set; }
        [Display(Name = "Medicine ")]
        public int MedicineId10 { get; set; }
        public string Dosage10 { get; set; }
        public bool Morning10 { get; set; }
        public bool Afternoon10 { get; set; }
        public bool Evening10 { get; set; }

        [Display(Name = "Check up after how many days")]
        public int CheckUp { get; set; }
        public DateTime? Date { get; set; }
    }
}