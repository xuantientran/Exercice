using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestUnitaire
{
	public interface ICity
	{
		string Name { get; }
		ICompany AddCompany(string name);
		ICompany FindCompany(string name);
	}
}
