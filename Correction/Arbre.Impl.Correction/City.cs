using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arbre
{
	internal class City : ICity
	{
		private readonly List<Company> _companies;

		internal City(string name)
		{
			if (String.IsNullOrEmpty(name)) throw new ArgumentNullException();

			_companies = new List<Company>();
			Name = name;
		}

		public string Name { get; private set; }

		public ICompany AddCompany(string name)
		{
			if (String.IsNullOrEmpty(name)) throw new ArgumentNullException();

			if (_companies.Where(c => c.Name == name).Any())
			{
				throw new ArgumentException("There is already a company with this name.");
			}
			Company company = new Company(name, this);
			_companies.Add(company);
			return company;
		}

		public ICompany FindCompany(string name)
		{
			return _companies.Where(c => c.Name == name).FirstOrDefault();
		}

	}
}
