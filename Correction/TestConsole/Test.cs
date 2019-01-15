using ITI.DSNTree;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;


namespace TestConsole
{
	public class Test
	{
		static string GetThisFilePath([CallerFilePath]string path = null) => path;
		static string GetSolutionDirectory() => Path.GetDirectoryName(Path.GetDirectoryName(Path.GetDirectoryName(GetThisFilePath())));
		static string GetDataDirectory() => Path.Combine(GetSolutionDirectory(), "Data");

		public static void TestTree()
		{
			var path = Path.Combine(GetDataDirectory(), "Example0", "dsn.txt");
			IDsnTree dsnTree = DsnTreeFactory.LoadTree(path);
			var l = ((DsnTree)dsnTree).GetPathToNode("S40.G05.00");
			foreach (var n in l)
			{
				Console.WriteLine(n);
			}
			/*
			foreach (var n in ((DSNTree)tree).DsnNodes)
				Console.WriteLine(n.Key);
			foreach(var n in tree.Root.Children)
			{
				Console.WriteLine(n.Id);
			}
			Utilitaire.Traverse(tree.Root);
			*/
		}

		public static void TestDsnData()
		{
			IDsnData dsnData = DsnTreeFactory.LoadDsnData(Path.Combine(GetDataDirectory(), "Example0", "dsn.txt"), @"D:\Test_workspace.Net\Net4DS\Donnee\DADSU.V01X13.Loic.POMPILI CHARTRON.S60.txt");

			foreach (var e in dsnData.Employees)
			{
				Console.WriteLine(e.Key + " " + e.Value.FirstName + " " + e.Value.LastName);
				foreach (var p in e.Value.ActivityPeriods)
				{
					Console.WriteLine(p.BeginDate);
				}
			}
		}

	}
}
