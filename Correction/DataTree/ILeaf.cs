using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.DATATree
{
	public interface ILeaf
	{
		Dictionary<string,string> LeafData { set; get; }
		IBlock Bloc { set; get; }
	}
}
