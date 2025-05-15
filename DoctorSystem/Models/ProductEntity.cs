using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DoctorSystem.Models
{
	public class ProductEntity
	{
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }
		public string product { get; set; }
		public long Price { get; set; }
		public long quantity { get; set; }
    }
}