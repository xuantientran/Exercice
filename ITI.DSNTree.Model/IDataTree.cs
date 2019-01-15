using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.DSNTree
{
	public interface IDataTree
	{
		IDataBlock Root { set; get; }
		int Count { get; }
		IDsnTree DsnTree { get; }
	}
}
