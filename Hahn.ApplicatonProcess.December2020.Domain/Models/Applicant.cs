namespace Hahn.ApplicatonProcess.December2020.Domain.Models
{
	public class Applicant
	{
		public int ID { get; set; }
		public string Name { get; set; }
		public string FamilyName { get; set; }
		public string Address { get; set; }
		public string CountryOfOrigin { get; set; }
		public string EMailAddress { get; set; }
		public int Age { get; set; }
		public bool Hired { get; set; }

		//needed by Ef Core
		private Applicant()
		{ }

		public Applicant(string name, string familyName, string address, string countryOfOrigin, string eMailAddress, 
			int age, bool isHired)
		{
			Name = name;
			FamilyName = familyName;
			Address = address;
			CountryOfOrigin = countryOfOrigin;
			EMailAddress = eMailAddress;
			Age = age;
			Hired = isHired;
		}

		public void Update(string name, string familyName, string address, string countryOfOrigin, string eMailAddress,
			int age, bool isHired)
        {
			Name = name;
			FamilyName = familyName;
			Address = address;
			CountryOfOrigin = countryOfOrigin;
			EMailAddress = eMailAddress;
			Age = age;
			Hired = isHired;
        }
	}
}
