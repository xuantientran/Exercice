using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;

namespace TestUnitaire.Tests
{
	[TestFixture]
	public class Test
	{
		[Test]
		public void T1_companies_can_be_found_by_name()
		{
			ICity s = CityFactory.CreateCity("Paris");
			ICompany c1 = s.AddCompany("SNCF");
			s.FindCompany("SNCF").Should().BeSameAs(c1);
			s.FindCompany("RATP").Should().BeNull();
		}
	}
}
