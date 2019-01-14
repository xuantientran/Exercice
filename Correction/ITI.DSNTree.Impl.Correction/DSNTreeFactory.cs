using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.DSNTree
{
	public class DSNTreeFactory
	{
		public static IDSNTree LoadTree( string path )
		{
			DSNTree tree = new DSNTree(path);
			return tree;
		}
	}
}
