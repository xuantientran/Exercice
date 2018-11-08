using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arbre
{
	internal class Company : ICompany
	{
		internal Company(string name, ICity city)
		{
			if (String.IsNullOrEmpty(name)) throw new ArgumentException();
			Name = name;
			City = city;
		}
		public string Name { get; private set; }
		public ICity City { get; private set; }
	}
}
