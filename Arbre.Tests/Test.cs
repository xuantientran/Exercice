using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;

namespace Arbre.Tests
{
	[TestFixture]
	public class Test
	{
		[Test]
		public void VerifierNoeud()
		{
			IDsn dsn = FactoryArbre.CreerArbre();
			FactoryArbre.FichiersEgaux().Should().BeTrue();
			INoeud parent = dsn.ObtenirNoeud("S30.G01.00");
			parent.Should().NotBeNull();
			INoeud noeud = dsn.ObtenirNoeud("S40.G01.00");
			noeud.Should().NotBeNull();
			noeud.Parent.Should().BeSameAs(parent);
			noeud.Enfants.Count.Should().Be(41);
		}
	}
}
