using System;
using System.Collections.Generic;

namespace ITI.DSNTree
{
	public interface IDataBlock
	{
		string Id { set; get; }
		IDataBlock Parent { set; get; }
		List<IDataBlock> Children { set; get; }
		List<IDataLeaf> Leaves { set; get; }
	}
}