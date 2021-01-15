namespace Hahn.ApplicatonProcess.December2020.Web.Controllers.Applicants
{
    //This is the Applicant DTO and it should be used
    //for any kind of transfer of Applicant DomainModel
    public class ApplicantResponse
    {
		public int ID { get; set; }
		public string Name { get; set; }
		public string FamilyName { get; set; }
		public string Address { get; set; }
		public string CountryOfOrigin { get; set; }
		public string EMailAddress { get; set; }
		public int Age { get; set; }
		public bool Hired { get; set; }
	}
}
