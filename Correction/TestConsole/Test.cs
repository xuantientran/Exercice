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
			var path = Path.Combine(GetDataDirectory(), "Example0", "TestData.txt");
			IDSNTree tree = DSNTreeFactory.LoadTree(path);
			/*
			foreach (var n in ((DSNTree)tree).DsnNodes)
				Console.WriteLine(n.Key);
			foreach(var n in tree.Root.Children)
			{
				Console.WriteLine(n.Id);
			}
			*/
			Utilitaire.Traverse(tree.Root);
		}

	}
}
