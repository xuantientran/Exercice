using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestUnitaire
{
	public class CityFactory
	{
		public static ICity CreateCity(string name)
		{
			return new City(name);
		}
	}
}
