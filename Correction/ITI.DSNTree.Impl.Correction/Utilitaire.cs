using DiffPlex;
using DiffPlex.DiffBuilder;
using DiffPlex.DiffBuilder.Model;
using ITI.DSNTree;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestConsole
{
	public class Utilitaire
	{
		public static void Diff()
		{
			StringBuilder sb = new StringBuilder();

			string oldText = @"We the people
                of the united states of america
                establish justice
                ensure domestic tranquility
                provide for the common defence
                secure the blessing of liberty
                to ourselves and our posterity";
			string newText = @"We the peaple
                in order to form a more perfect union
                establish justice
                ensure domestic tranquility
                promote the general welfare and
                secure the blessing of liberty
                to ourselves and our posterity
                do ordain and establish this constitution
                for the United States of America";

			var d = new Differ();
			var builder = new InlineDiffBuilder(d);
			var result = builder.BuildDiffModel(oldText, newText);

			foreach (var line in result.Lines)
			{
				if (line.Type == ChangeType.Inserted)
				{
					sb.Append("+ ");
				}
				else if (line.Type == ChangeType.Deleted)
				{
					sb.Append("- ");
				}
				else if (line.Type == ChangeType.Modified)
				{
					sb.Append("* ");
				}
				else if (line.Type == ChangeType.Imaginary)
				{
					sb.Append("? ");
				}
				else if (line.Type == ChangeType.Unchanged)
				{
					sb.Append("  ");
				}

				sb.Append(line.Text + "\n");
			}
			Console.WriteLine(sb.ToString());
		}

		public static void CompaireActivityPeriod(IDataTree dataTree, IDataBlock activityPeriodDataBlock, IDataBlock activityPeriodDataBlock2)
		{
			IDsnNode current = dataTree.DsnTree.Find(ActivityPeriod.KeyActivityPeriodBlock);

			Stack<IDsnNode> stack = new Stack<IDsnNode>();
			stack.Push(current);
			while (stack.Count > 0)
			{
				current = stack.Pop();
				for (var i = current.Children.Count - 1; i >= 0; i--)
					stack.Push(current.Children[i]);
				
			}


		}
		public static void Traverse(IDsnNode root)
		{
			//, Encoding.GetEncoding("iso-8859-1")
			using (StreamWriter writer = new StreamWriter("Traverse.txt", false))
			{
				IDsnNode current = root;
				Stack<IDsnNode> stack = new Stack<IDsnNode>();
				stack.Push(current);
				while (stack.Count > 0)
				{
					current = stack.Pop();
					if (current == null)
						continue;

					for (var i = current.Children.Count - 1; i >= 0; i--)
						stack.Push(current.Children[i]);

					//Do something with current
					string strMax = (current.Cardinality.Max == 0) ? "*" : current.Cardinality.Max.ToString();
					writer.WriteLine(Tabs(current.Level) + current.Id + " - " + current.Label + "(" + current.Cardinality.Min + "," + strMax + ")");
				}
			}
		}

		public static string Tabs(int numTabs, string tab = "\t")
		{
			StringBuilder sb = new StringBuilder();
			for (int i = 0; i < numTabs; i++)
				sb.Append(tab);

			return sb.ToString();
		}

	}
}
