using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;

namespace ITI.DSNTree.Tests
{
	[TestFixture]
	public class Test
	{
		static string GetThisFilePath([CallerFilePath]string path = null) => path;
		static string GetSolutionDirectory() => Path.GetDirectoryName(Path.GetDirectoryName(GetThisFilePath()));
		static string GetDataDirectory() => Path.Combine(GetSolutionDirectory(), "Data");
		static string result = Path.Combine(GetDataDirectory(), "Example0", "result.txt");

		[Test]
		public void T00_loading_simple_test_tree()
		{
			string path = Path.Combine(GetDataDirectory(), "Example0", "dsnTree1.txt");
			IDsnTree tree = DsnTreeFactory.LoadTree(path);
			string id1 = "S10.G01.00";
			tree.Should().NotBeNull();
			tree.Count.Should().Be(1);
			IDsnNode dsnNode1 = tree.Find(id1);
			dsnNode1.Label.Should().Be("Emetteur");
			dsnNode1.Cardinality.Should().Be(new DsnCardinality(1, 1));
			dsnNode1.Level.Should().Be(1);
			dsnNode1.Id.Should().Be(id1);
			CheckFileEquals(path, tree.ToString());
		}

		[Test]
		public void T01_loading_hierachy_test_tree()
		{
			string path = Path.Combine(GetDataDirectory(), "Example0", "dsnTree2.txt");
			IDsnTree tree = DsnTreeFactory.LoadTree(path);
			string id1 = "S10.G01.00";
			tree.Should().NotBeNull();
			tree.Count.Should().Be(2);
			IDsnNode dsnNode1 = tree.Find(id1);
			dsnNode1.Label.Should().Be("Emetteur");
			dsnNode1.Cardinality.Should().Be(new DsnCardinality(1, 1));
			dsnNode1.Level.Should().Be(1);
			dsnNode1.Id.Should().Be(id1);

			string id2 = "S10.G01.01";
			IDsnNode dsnNode2 = tree.Find(id2);
			dsnNode2.Label.Should().Be("Contacts Emetteur");
			dsnNode2.Cardinality.Should().Be(new DsnCardinality(1, 0));
			dsnNode2.Parent.Should().Be(dsnNode1);
			dsnNode2.Level.Should().Be(2);
			dsnNode2.Id.Should().Be(id2);
			CheckFileEquals(path, tree.ToString());
		}

		[Test]
		public void T02_loading_full_test_tree()
		{
			string path = Path.Combine(GetDataDirectory(), "Example0", "dsnTree3.txt");
			IDsnTree tree = DsnTreeFactory.LoadTree(path);
			string id1 = "S10.G01.00";
			tree.Should().NotBeNull();
			tree.Count.Should().Be(104);
			IDsnNode dsnNode1 = tree.Find(id1);
			dsnNode1.Label.Should().Be("Emetteur");
			dsnNode1.Cardinality.Should().Be(new DsnCardinality(1, 1));
			dsnNode1.Level.Should().Be(1);
			dsnNode1.Id.Should().Be(id1);
			dsnNode1.Children.Count.Should().Be(3);

			string id2 = "S45.G05.10";
			IDsnNode dsnNode2 = tree.Find(id2);
			dsnNode2.Label.Should().Be("Période de cotisation");
			dsnNode2.Cardinality.Should().Be(new DsnCardinality(1, 0));
			dsnNode2.Level.Should().Be(7);
			dsnNode2.Id.Should().Be(id2);
			dsnNode2.Children.Count.Should().Be(3);
			CheckFileEquals(path, tree.ToString());
		}

		static void CheckFileEquals(string path, string text)
		{
			File.ReadAllText(path).Trim().Should().Be(text.Trim());
		}
	}
}
