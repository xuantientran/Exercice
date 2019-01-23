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

		public static IDsnTree LoadTree( string path )
		{
			return new DsnTree(path);
		}

		public static IDataTree LoadDataTree(IDsnTree dsnTree, string path)
		{
			IDataTree dataTree = new DataTree(dsnTree);
			((DataTree)dataTree).LoadDataTree(path);
			return dataTree;
		}

		public static IDsnData LoadData(IDataTree dataTree) => new DsnData(dataTree);
	}
}
