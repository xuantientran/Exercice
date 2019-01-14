using System;
using System.Collections.Generic;

namespace ITI.DATATree
{
	public interface IBlock
	{
		string Id { set; get; }
		List<ILeaf> Leaves { set; get; }
	}
}
