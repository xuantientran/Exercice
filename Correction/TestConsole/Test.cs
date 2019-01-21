using DiffPlex;
using DiffPlex.DiffBuilder;
using DiffPlex.DiffBuilder.Model;
using ITI.DSNTree;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

		static string pathDsnTree = Path.Combine(GetDataDirectory(), "Example0", "dsn.txt");
		static string pathDataTree = Path.Combine(GetDataDirectory(), "Example0", "TestData.txt");
		static string pathDataTree2 = Path.Combine(GetDataDirectory(), "Example0", "TestData2.txt");
		static string pathResult = Path.Combine(GetDataDirectory(), "Example0", "result.txt");

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

		public static void TestToString()
		{
			using (StreamWriter writer = new StreamWriter(pathResult, false))
			{
				IDsnTree dsnTree = DsnTreeFactory.LoadTree(pathDsnTree);
				IDataTree dataTree = DsnTreeFactory.loadDataTree(dsnTree, pathDataTree);
				IDsnData dsnData = new DsnData(dataTree);
				writer.Write(dsnData.DataDispatch.ToString());
			}
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
			//string pathDataTree = @"D:\Donnees_VParis\PSBDADSU_VIL.C11_180126_003118_9.026.756_77.327.txt";
			//string pathDataTree2 = @"D:\Donnees_VParis\PSBDADSU_VIL.C11_180126_003118_9.026.756_77.327.modifie.txt";

			Stopwatch muniteur = new Stopwatch();
			muniteur.Start();

			using (StreamWriter writer = new StreamWriter(pathResult, false))
			{
				IDsnTree dsnTree = DsnTreeFactory.LoadTree(pathDsnTree);
				IDataTree dataTree = DsnTreeFactory.loadDataTree(dsnTree, pathDataTree);
				IDataTree dataTree2 = DsnTreeFactory.loadDataTree(dsnTree, pathDataTree2);
				IDsnData dsnData = new DsnData(dataTree);
				IDsnData dsnData2 = new DsnData(dataTree2);
				List<DataItem> dataItems = new List<DataItem>();
				foreach (var employee in dsnData.Employees)
				{
					if (dsnData2.Employees.ContainsKey(employee.Key))
					{
						var employee2 = dsnData2.Employees[employee.Key];
						Comparison.CompareDictionary(employee.Value.EmployeeDataBlock.Leaves.First().Data, employee2.EmployeeDataBlock.Leaves.First().Data, dataItems);

						foreach (var activityPeriod in employee.Value.ActivityPeriods)
						{
							var activityPeriod2 = employee2.ActivityPeriods.Find(item => item.BeginDate.Equals(activityPeriod.BeginDate));
							if (activityPeriod2 != null)
								Comparison.TextDiff(activityPeriod2.ToString(), activityPeriod.ToString(), dataItems, true);
						}
					}
				}
				foreach (var item in dataItems)
				{
					switch (item.Status)
					{
						case ChangeStatus.Deleted:
							writer.Write("- ");
							break;
						case ChangeStatus.Inserted:
							writer.Write("+ ");
							break;
						case ChangeStatus.Modified:
							writer.Write("* ");
							break;
						default:
							writer.Write("  ");
							break;
					}
					writer.WriteLine(item.Key + ",'" + item.Value + "','" + item.OldValue + "'");
				}
			}
			muniteur.Stop();
			Console.WriteLine("Comparision in " + muniteur.Elapsed + " seconds");
		}
	}
}
