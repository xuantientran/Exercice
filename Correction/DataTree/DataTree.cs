using ITI.DSNTree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.DATATree
{
	public class DataTree : IDataTree
	{
		IBlock _root;
		IDSNTree _dsnTree;

		public DataTree(IDSNTree dsnTree, string path)
		{
			_dsnTree = dsnTree;
		}

		void LoadDataTree(string path)
		{

		}

		public IBlock Root { get => _root; set => _root = value; }
	}
}
