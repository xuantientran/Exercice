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
		public static void Traverse(IDSNNode root)
		{
			//, Encoding.GetEncoding("iso-8859-1")
			using (StreamWriter writer = new StreamWriter("Traverse.txt", false))
			{
				IDSNNode current = root;
				Stack<IDSNNode> stack = new Stack<IDSNNode>();
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
