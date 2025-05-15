using DoctorSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoctorSystem.CollectionViewModels
{
	public class UploadTestsCollection
	{
		public UploadTestResults UploadTestResults { get; set; }
		public List<TestType> TestTypes { get; set; }
	}
}