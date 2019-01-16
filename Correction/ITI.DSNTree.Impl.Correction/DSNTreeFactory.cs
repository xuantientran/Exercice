using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.DSNTree
{
	public class DsnTreeFactory
	{
		public static IDsnTree LoadTree(string path)
		{
			DsnTree dsnTree = new DsnTree(path);
			return dsnTree;
		}

		public static IDataTree loadDataTree(IDsnTree dsnTree, string path)
		{
			IDataTree dataTree = new DataTree(dsnTree);
			((DataTree)dataTree).LoadDataTree(path);
			return dataTree;
		}

		public static IDsnData LoadDsnData(string path, string dataPath)
		{
			IDsnTree dsnTree = new DsnTree(path);
			IDataTree dataTree = new DataTree(dsnTree);
			((DataTree)dataTree).LoadDataTree(dataPath);
			IDsnData dsnData = new DsnData(dataTree);
			return dsnData;
		}
	}
}
