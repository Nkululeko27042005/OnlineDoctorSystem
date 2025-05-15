using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DoctorSystem.Models
{
	public class Doctor
	{
        [Key]
        public int Id { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }
        public string ApplicationUserId { get; set; }

        public string FullName { get; set; }


        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [EmailAddress]
        public string EmailAddress { get; set; }

        public string Address { get; set; }
        [Display(Name = "TelePhone Number")]
        public string PhoneNo { get; set; }

        [Display(Name = "Mobile Number")]
        public string ContactNo { get; set; }

        public string Specialization { get; set; }

        public string Gender { get; set; }

        [Display(Name = "Education/Degree")]
        public string Education { get; set; }
    }
}