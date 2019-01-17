using DiffPlex;
using DiffPlex.DiffBuilder;
using DiffPlex.DiffBuilder.Model;
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
			using (StreamWriter writer = new StreamWriter("TestDsnData.txt", false))
			{

				//IDsnData dsnData = DsnTreeFactory.LoadDsnData(Path.Combine(GetDataDirectory(), "Example0", "dsn.txt"), @"D:\Test_workspace.Net\Net4DS\Donnee\DADSU.V01X13.Loic.POMPILI CHARTRON.S60.txt");
				IDsnData dsnData
					= DsnTreeFactory.LoadDsnData(Path.Combine(GetDataDirectory(), "Example0", "dsn.txt"),
					Path.Combine(GetDataDirectory(), "Example0", "TestData.txt"));
				foreach (var p in dsnData.HonorairePayers)
				{
					writer.WriteLine(p.Nic);
					foreach (var h in p.Honoraires)
					{
						writer.WriteLine(h.Quality);
					}
				}
				/*
				foreach (var e in dsnData.Employees)
				{
					writer.WriteLine("------------------");
					writer.WriteLine(e.Key + " " + e.Value.FirstName + " " + e.Value.LastName);
					writer.WriteLine("-Activity periods-");
					foreach (var a in e.Value.ActivityPeriods)
						writer.WriteLine(a.BeginDate);
					writer.WriteLine("-Special periods-");
					foreach (var s in e.Value.SpecialPeriods)
						writer.WriteLine(s.BeginDate);
				}
				*/
			}
		}

		public static void TestComparison()
		{
			string pathDsnTree = Path.Combine(GetDataDirectory(), "Example0", "dsn.txt");
			string pathDataTree = Path.Combine(GetDataDirectory(), "Example0", "V01X13.Myriam RENAULD.txt");
			string pathDataTree2 = Path.Combine(GetDataDirectory(), "Example0", "V01X13.Myriam RENAULD.modified.txt");
			string pathResult = Path.Combine(GetDataDirectory(), "Example0", "result.txt");
			using (StreamWriter writer = new StreamWriter(pathResult, false))
			{
				IDsnTree dsnTree = DsnTreeFactory.LoadTree(pathDsnTree);
				IDataTree dataTree = DsnTreeFactory.loadDataTree(dsnTree, pathDataTree);
				IDataTree dataTree2 = DsnTreeFactory.loadDataTree(dsnTree, pathDataTree2);
				IDsnData dsnData = new DsnData(dataTree);
				IDsnData dsnData2 = new DsnData(dataTree2);

				StringBuilder sb = new StringBuilder();
				var d = new Differ();
				var builder = new InlineDiffBuilder(d);

				foreach (var e in dsnData.Employees)
				{
					if (dsnData2.Employees.ContainsKey(e.Key))
					{
						for (var i = 0; i < e.Value.ActivityPeriods.Count; i++)
						{
							var a = e.Value.ActivityPeriods[i].ToString();
							var a2 = dsnData2.Employees[e.Key].ActivityPeriods[i].ToString();
							var result = builder.BuildDiffModel(a, a2);

							DiffPiece lastDiffPiece = new DiffPiece { Type = ChangeType.Unchanged };
							result.Lines[-1] = lastDiffPiece;
							for (int ix = 0; ix < result.Lines.Count; ix++)
							{
								switch (result.Lines[ix].Type)
								{
									case ChangeType.Inserted:
										if (lastDiffPiece.Type == ChangeType.Deleted)
											sb.AppendLine("* " + result.Lines[ix].Text);
										else
											sb.AppendLine("+ " + result.Lines[ix].Text);
										break;
									case ChangeType.Deleted:
										sb.AppendLine("- " + result.Lines[ix].Text);
										break;
									case ChangeType.Modified:
										sb.AppendLine("* " + result.Lines[ix].Text);
										break;
									case ChangeType.Unchanged:
										sb.AppendLine("  " + result.Lines[ix].Text);
										break;
									default:
										sb.AppendLine("? " + result.Lines[ix].Text);
										break;
								}
								lastDiffPiece = result.Lines[ix];
							}
							writer.Write(sb.ToString());
						}
					}
				}
			}
		}
	}
}
